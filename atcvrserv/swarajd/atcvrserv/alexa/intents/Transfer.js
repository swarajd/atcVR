const lib = require('lib');
const request = require('superagent');

module.exports = function (slots, callback) {

    request
        .post('https://limitless-oasis-38724.herokuapp.com/transfer')
        .send({
            pid: slots.planea.value,
            dir: slots.dir.value
        })
        .set('Accept', 'application/json')
        .end((err, res) => {
            if (err || !res.ok) {
                return callback(null, `error sending transfer data to server!`);
            } else {
                return callback(null, `${slots.planea.value} transferred ${slots.dir.value}`);
            }
        })

};
