const TextLintEngine = require('textlint').TextLintEngine;
const express = require('express');
const bodyParser = require('body-parser');
const app = express();

// postデータのjsonをパースするおまじない
app.use(bodyParser.urlencoded({
    extended: true
}));
app.use(bodyParser.json());

// 8081番ポートで待ちうける
app.listen(8081, () => {
    console.log('Running at Port 8081...');
});

app.post('/api/textlint', (req, res, next) => {
    const req_text = req.body.text;
    const engine = new TextLintEngine();
    engine.executeOnText(req_text).then(results => {
        res.json({
            messages: results[0].messages
        });
    });
});

// その他のリクエストに対する404エラー
app.use((req, res) => {
    res.sendStatus(404);
});