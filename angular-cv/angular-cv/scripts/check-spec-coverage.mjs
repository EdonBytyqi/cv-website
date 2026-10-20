// Verifies that every functional requirement in an Approved or Implemented Spec Kit
// feature (repo-root specs/NNN-name/spec.md) that applies to the Angular app has at
// least one test, and that tests only reference requirements that exist.
// Tests reference a requirement as "[NNN:FR-###]", e.g. it('[001:FR-004] ...').
// Usage: node scripts/check-spec-coverage.mjs
import { readFileSync, readdirSync } from 'node:fs';
import { join, relative } from 'node:path';

const appRoot = join(import.meta.dirname, '..');
const repoRoot = join(appRoot, '..', '..');
const TRACKED_STATUSES = new Set(['Approved', 'Implemented']);
const OTHER_APP_ONLY = '*(.NET only)*';
const REQUIREMENT = /^- \*\*(FR-\d{3})\*\*:(.*)$/gm;
const TEST_REFERENCE = /\[(\d{3}:FR-\d{3})\]/g;

function filesUnder(dir, predicate) {
  return readdirSync(dir, { recursive: true, withFileTypes: true })
    .filter((entry) => entry.isFile() && predicate(entry.name))
    .map((entry) => join(entry.parentPath, entry.name));
}

const tracked = new Map(); // key -> spec file
const known = new Set();

for (const feature of readdirSync(join(repoRoot, 'specs'), { withFileTypes: true })) {
  const number = feature.isDirectory() && feature.name.match(/^(\d{3})-/)?.[1];
  if (!number) continue;

  const file = join(repoRoot, 'specs', feature.name, 'spec.md');
  const text = readFileSync(file, 'utf8');
  const status = text.match(/^\*\*Status\*\*:\s*(\w+)/m)?.[1] ?? 'Draft';
  for (const [, id, description] of text.matchAll(REQUIREMENT)) {
    const key = `${number}:${id}`;
    known.add(key);
    if (TRACKED_STATUSES.has(status) && !description.includes(OTHER_APP_ONLY)) {
      tracked.set(key, relative(repoRoot, file));
    }
  }
}

const tested = new Set();
const unknown = [];

for (const file of filesUnder(join(appRoot, 'src'), (name) => name.endsWith('.spec.ts'))) {
  for (const [, key] of readFileSync(file, 'utf8').matchAll(TEST_REFERENCE)) {
    tested.add(key);
    if (!known.has(key)) unknown.push(`${key} in ${relative(appRoot, file)}`);
  }
}

const untested = [...tracked].filter(([key]) => !tested.has(key)).map(([key, spec]) => `${key} (${spec})`);

for (const line of untested) console.error(`✗ No Angular test for ${line}`);
for (const line of unknown) console.error(`✗ Test references unknown requirement ${line}`);

if (untested.length > 0 || unknown.length > 0) process.exit(1);

console.log(`✓ All ${tracked.size} Angular requirements in specs/ have tests.`);
