
var amqp = require('amqplib/callback_api');

var args = ["user.add", "punch.add", "punch.discount"];

amqp.connect('amqp://localhost', function(err, conn) {
  conn.createChannel(function(err, ch) {
    var ex = 'punchcardApi';

    ch.assertExchange(ex, 'topic', {durable: false});

    ch.assertQueue('', {exclusive: true}, function(err, q) {

      args.forEach(function(key) {
        ch.bindQueue(q.queue, ex, key);
      });

      ch.consume(q.queue, function(msg) {
        if(msg.fields.routingKey === "user.add") {
          console.log("User was added");
        }
        else if(msg.fields.routingKey === "punch.add") {
          console.log("User got a punch");
        }
        else if(msg.fields.routingKey === "punch.discount") {
          console.log("User got discount");
        }
        console.log("%s", msg.content);
      }, {noAck: true});
    });
  });
});