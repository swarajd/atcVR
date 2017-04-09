let express = require('express');
let app = express();
let bodyParser = require('body-parser');


let gblLane = 'n';

app.get('/lane', function(req, res){
  res.json({
      lane: gblLane
  });
});

app.post('/lane', function(req, res) {
  let dat = req.body;

  let lane = parseInt(dat.lane);

  gblLane = lane;
  res.send('thx');
})

app.listen(3000);