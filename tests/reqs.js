const rq = require('superagent');

let getData = () => rq.get('https://limitless-oasis-38724.herokuapp.com/');

let dataUpdate = async function() {
    try {
        let dat = await getData();
        console.log(JSON.stringify(JSON.parse(dat.text),null,2));
    } catch (e) {
        console.log(e);
    }
}

let interval = setInterval(dataUpdate, 1000);

setTimeout(() => clearInterval(interval), 12000);