let express = require('express');
let app = express();
let bodyParser = require('body-parser');

app.use(bodyParser.urlencoded({ extended: false }))
app.use(bodyParser.json());

let globl = [];
let timer = 0;
let collision = false;
let transferError = false;

let gblLane = 'a';
let curPlane = 1;

const COLLISION = 20;
const TRANSFER  = 10;

const getEle = () => Math.floor(Math.random() * 10001);
const getXY  = () => Math.floor(Math.random() * 601) + 200;
const getSpd = () => Math.floor(Math.random() * 15) + 1;
const getDir = () => Math.random() * 2 * Math.PI;
const getVal = () => Math.floor(Math.random() * 21) - 10;

const getCmp = (spd, dir) => {
    let x = Math.floor(spd * Math.cos(dir));
    let y = Math.floor(spd * Math.sin(dir));

    return [x, y]
}

const rotate90 = (dir) => {
    dir += Math.PI/2;
    if (dir > 2 * Math.PI) {
        dir -= (2 * Math.PI);
    }
    return dir;
}

const collide2d = (cpl, gl) => {
    let collisions = gl.filter(pl => {
        return Math.sqrt(
            Math.pow(cpl.x - pl.x, 2) + 
            Math.pow(cpl.y - pl.y, 2)
        ) <= 20
    });
    let ln = collisions.length;
    return ln!= 0
}

const collide3d = (p1, p2) => {
    let p1x = p1.x * 1609.34
    let p1y = p1.y * 1609.34

    let p2x = p2.x * 1609.34
    let p2y = p2.y * 1609.34

    let p1z = p1.z;
    let p2z = p2.z;

    let distBetween = Math.sqrt(
            Math.pow(p2x - p1x, 2) + 
            Math.pow(p2y - p1y, 2) + 
            Math.pow(p2z - p1z, 2)
        );
    
    return distBetween < 3218.688;
}

let interval = null;

let dataUpdate = function() {
    globl.forEach(pl => {
        let cmp = getCmp(pl.spd, pl.dir);
        pl.x += cmp[0];
        pl.y += cmp[1];
        if ((pl.x < 0 || pl.x > 1000 || pl.y < 0 || pl.y > 1000) && pl.transfer != true) {
            // console.log(pl.x, pl.y, pl.transfer);
            // pl.x = getXY();
            // pl.y = getXY();
            transferError = true;
        }
    });

    checkCollisions(globl);
    // console.log(`collision: ${collision}, transfer: ${transferError}`);

    timer += 1000;
}

const checkCollisions = (gbl) => {
    gbl.forEach((p1, i) => {
        gbl.forEach((p2, j) => {
            if (i != j) {
                if (collide3d(p1, p2)) {
                    collision = true;
                    clearInterval(interval);
                }
            }
        })
    })
}

const initPlanes = () => {
    globl = [];
    timer = 0;
    collision = false;
    transferError = false;

    for (let i = 0; i < 20; i++) {

        // console.log('generate');
        let curPl = {
            id: 'plane'+i,
            x: getXY(),
            y: getXY(),
            z: getEle(),
            spd: getSpd(),
            dir: getDir(),
            transfer: false,
        };

        while (collide2d(curPl, globl)) {
            // console.log('collide');
            curPl = {
                id: 'plane'+i,
                x: getXY(),
                y: getXY(),
                z: getEle(),
                spd: getSpd(),
                dir: getDir(),
                transfer: false,
            };
        }

        globl.push(curPl);
    }

    console.log(JSON.stringify(globl, null, 2));

    // console.log(`[init] collision: ${collision}, transfer: ${transferError}`);
    interval = setInterval(dataUpdate, 1000);

}

initPlanes();





// setTimeout(() => clearInterval(interval), 12000);


app.get('/', function(req, res){
    let dat = {
      data: globl,
      collision: collision,
      transferError: transferError
    };
    res.json(dat);
});

app.get('/reset', function(req, res) {
    initPlanes();
    res.send('reset planes!');
})

app.post('/collide', function(req, res) {
    let dat = req.body;
    // console.log(dat);
    let p1id = parseInt(dat.p1id);
    let p2id = parseInt(dat.p2id);

    if (p1id >= globl.length || p2id >= globl.length) {
        res.send('one or more planes is incorrect');
    }

    let pid = -1;
    
    if (Math.floor(Math.random() * 2) == 0) {
        pid = p1id;
    } else {
        pid = p2id;
    }

    globl[pid].dir = rotate90(globl[pid].dir);

    // console.log("omg it worked!!!!");
    res.send('thx');
});

app.post('/transfer', function(req, res) {
    let dat = req.body;
    // console.log(dat);
    let pid = parseInt(dat.pid);
    let dir = parseInt(dat.dir);

    globl[pid].transfer = true;
    res.send('thx');
});

app.get('/lane', function(req, res){
  res.json({
      lane: gblLane,
      plane: curPlane
  });
});

app.post('/lane', function(req, res) {
  let dat = req.body;

  let lane = dat.lane;
  let plane = parseInt(dat.plane);

  gblLane = lane;
  curPlane = plane;
  res.send('thx');
})

app.listen(3000);