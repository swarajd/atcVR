const lib = require('lib');
const request = require('superagent');

module.exports = function (slots, callback) {

    request
        .post('https://limitless-oasis-38724.herokuapp.com/collide')
        .send({
            p1id: slots.planea.value,
            p2id: slots.planeb.value
        })
        .set('Accept', 'application/json')
        .end((err, res) => {
            if (err || !res.ok) {
                return callback(null, `error sending collision data to server! ${err}`);
            } else {
                return callback(null, `${slots.planea.value} avoided collision with ${slots.planeb.value}`);
            }
        })
    

};
