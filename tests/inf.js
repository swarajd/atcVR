

let dataUpdate = function() {
    console.log(250);
}

let interval = setInterval(dataUpdate, 250);

setTimeout(() => clearInterval(interval), 1275);
