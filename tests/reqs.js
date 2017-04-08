const rq = require('superagent');

let getData = () => rq.get('http://localhost:3000/');

let dataUpdate = async function() {
    try {
        let dat = await getData();
        console.log(JSON.stringify(JSON.parse(dat.text),null,2));
    } catch (e) {
        console.log(e);
    }
}

let interval = setInterval(dataUpdate, 250);

setTimeout(() => clearInterval(interval), 12000);