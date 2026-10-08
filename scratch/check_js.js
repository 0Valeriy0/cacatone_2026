const fs = require('fs');
const path = require('path');
const vm = require('vm');

const viewsDir = path.join(__dirname, '..', 'cacatone_2026', 'Views', 'Home');
const files = fs.readdirSync(viewsDir).filter(f => f.endsWith('.cshtml'));

let totalErrors = 0;

files.forEach(file => {
  const filePath = path.join(viewsDir, file);
  const content = fs.readFileSync(filePath, 'utf8');

  // Extract <script>...</script> blocks
  const scriptRegex = /<script\b[^>]*>([\s\S]*?)<\/script>/gi;
  let match;
  let scriptIndex = 0;

  while ((match = scriptRegex.exec(content)) !== null) {
    scriptIndex++;
    let jsCode = match[1];

    // Replace Razor @@ with @ for JS parsing
    jsCode = jsCode.replace(/@@/g, '@');

    try {
      new vm.Script(jsCode, { filename: `${file}#script${scriptIndex}` });
      console.log(`✓ [PASS] ${file} script #${scriptIndex}`);
    } catch (err) {
      totalErrors++;
      console.error(`✕ [FAIL] ${file} script #${scriptIndex}:`, err.message);
    }
  }
});

console.log(`\nValidation complete. Total errors: ${totalErrors}`);
process.exit(totalErrors > 0 ? 1 : 0);
