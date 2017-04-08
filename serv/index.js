var express = require('express');
var app = express();

let globl = [];


const getEle = () => Math.floor(Math.random() * 2001);
const getXY  = () => Math.floor(Math.random() * 301);
const getSpd = () => Math.floor(Math.random() * 21);
const getDir = () => Math.random() * 2 * Math.PI;
const getVal = () => Math.floor(Math.random() * 21) - 10;

const getCmp = (spd, dir) => {
    let x = Math.floor(spd * Math.cos(dir));
    let y = Math.floor(spd * Math.sin(dir));

    return [x, y]
}

const collide = (cpl, gl) => {
    let collisions = gl.filter(pl => {
        return Math.sqrt(
            Math.pow(cpl.x - pl.x, 2) + 
            Math.pow(cpl.y - pl.y, 2)
        ) <= 20
    });
    let ln = collisions.length;
    return ln!= 0
}

for (let i = 0; i < 20; i++) {

    // console.log('generate');
    let curPl = {
        x: getXY(),
        y: getXY(),
        z: getEle(),
        spd: getSpd(),
        dir: getDir()
    };

    while (collide(curPl, globl)) {
        // console.log('collide');
        curPl = {
            x: getXY(),
            y: getXY(),
            z: getEle(),
            spd: getSpd(),
            dir: getDir()
        };
    }

    globl.push(curPl);
}

let dataUpdate = function() {
    globl.forEach(pl => {
        let cmp = getCmp(pl.spd, pl.dir);
        pl.x += cmp[0];
        pl.y += cmp[1];
    })
}

let interval = setInterval(dataUpdate, 1000);

setTimeout(() => clearInterval(interval), 12000);


app.get('/', function(req, res){
  res.json({
      data: globl
  });
});

app.listen(3000);