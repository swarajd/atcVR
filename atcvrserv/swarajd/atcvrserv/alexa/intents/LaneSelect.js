const lib = require('lib');
const request = require('superagent');

module.exports = function (slots, callback) {

    let lane = slots.laneNumber.value;
    lane = lane
            .replace(/\W/g, '')
            .toLowerCase();

    if (lane == null || slots.planea.value == null) {
        return callback(null, `say that again?`);
    }

    request
        .post('https://limitless-oasis-38724.herokuapp.com/lane/')
        .send({
            lane: lane,
            plane: slots.planea.value
        })
        .set('Accept', 'application/json')
        .end((err, res) => {
            if (err || !res.ok) {
                return callback(null, `error sending lane data to server!`);
            } else {
                return callback(null, `routing plane ${slots.planea.value} to lane ${slots.laneNumber.value}`);
            }
        })
    

};
