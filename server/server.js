var port = process.env.PORT || 3000;
var io = require('socket.io')(port);
var shortId = require('shortid');


console.log("server started on port " + port);

var RandomNum = Math.floor(Math.random() * 100);
console.log(RandomNum);

var playerNumConnected;
var playerNumReady;

var isPlaying = false;

/*port.listen(3000, function () {
    console.log("server started");
    
});*/


io.on("connection",function(socket){ 

    //playerNumConnected++;

    socket.emit('Connect To Server');
    
    socket.on('login', function (data){

        if(!isPlaying){
            playerNumConnected++;

            var thisPlayerId = shortId.generate();

            playerID = {id:thisPlayerId,name:data.plyerName,playerConnected:playerNumConnected}

            console.log("New Player! " + playerID);
            console.log(playerNumConnected + " player has connect");

            socket.emit('I Login', playerID);

            socket.broadcast.emit('Other Player Connected', playerID);
        }

    });

});