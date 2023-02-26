const TextLintEngine = require("textlint").TextLintEngine;

const engine = new TextLintEngine({
    rulePaths: [
      "node_modules/textlint-rule-preset-ja-technical-writing"
    ]
});

engine.executeOnText(process.argv[2]).then(results => {
  console.log(results[0].messages);

  if (engine.isErrorResults(results)) {
    const output = engine.formatResults(results);
    console.log(output);
  }
});