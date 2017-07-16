var amqp = require('amqplib/callback_api');

var sendToConsumer = function(key, msg) {
  amqp.connect('amqp://localhost', function(err, conn) {
    conn.createChannel(function(err, ch) {
      var ex = 'punchcardApi';
      
      ch.assertExchange(ex, 'topic', {durable: false});
      ch.publish(ex, key, new Buffer(JSON.stringify(msg, null, 4)));
      console.log("%s", key);
      console.log(JSON.stringify(msg, null, 4));
    });
  });
};

module.exports = {
  sendToConsumer: sendToConsumer
}