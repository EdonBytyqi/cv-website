// Verifies that every acceptance criterion in an Approved or Implemented spec
// is referenced by at least one test, and that tests only reference known IDs.
// Usage: node scripts/check-spec-coverage.mjs
import { readFileSync, readdirSync } from 'node:fs';
import { join, relative } from 'node:path';

const root = join(import.meta.dirname, '..');
const TRACKED_STATUSES = new Set(['Approved', 'Implemented']);
const CRITERION = /\*\*(AC-\d{3}-\d{2})\*\*/g;
const TEST_REFERENCE = /\[(AC-\d{3}-\d{2})\]/g;

function filesUnder(dir, predicate) {
  return readdirSync(dir, { recursive: true, withFileTypes: true })
    .filter((entry) => entry.isFile() && predicate(entry.name))
    .map((entry) => join(entry.parentPath, entry.name));
}

const tracked = new Map(); // id -> spec file
const allCriteria = new Set();

for (const file of filesUnder(join(root, 'specs'), (name) => name === 'spec.md')) {
  const text = readFileSync(file, 'utf8');
  const status = text.match(/^Status:\s*(\w+)/m)?.[1] ?? 'Draft';
  for (const [, id] of text.matchAll(CRITERION)) {
    allCriteria.add(id);
    if (TRACKED_STATUSES.has(status)) tracked.set(id, relative(root, file));
  }
}

const tested = new Set();
const unknown = [];

for (const file of filesUnder(join(root, 'src'), (name) => name.endsWith('.spec.ts'))) {
  for (const [, id] of readFileSync(file, 'utf8').matchAll(TEST_REFERENCE)) {
    tested.add(id);
    if (!allCriteria.has(id)) unknown.push(`${id} in ${relative(root, file)}`);
  }
}

const untested = [...tracked].filter(([id]) => !tested.has(id)).map(([id, spec]) => `${id} (${spec})`);

for (const line of untested) console.error(`✗ No test for ${line}`);
for (const line of unknown) console.error(`✗ Test references unknown criterion ${line}`);

if (untested.length > 0 || unknown.length > 0) process.exit(1);

console.log(`✓ All ${tracked.size} tracked acceptance criteria have tests.`);
