var port = process.env.PORT || 3000;
var io = require('socket.io')(port);
var shortId = require('shortid');


console.log("server started on port " + port);

var RandomNum = Math.floor(Math.random() * 100);
console.log(RandomNum);

var playerNumConnected = 0;
var playerNumReady;

var isPlaying = false;

/*port.listen(3000, function () {
    console.log("server started");
    
});*/


io.on('connection',function(socket){ 

    console.log("Some One Connect to Server");

    //playerNumConnected++;

    socket.emit('Connect To Server');
    
    socket.on('login', function (data){

        if(!isPlaying){
            playerNumConnected++;

            var thisPlayerId = shortId.generate();

            playerID = {id:thisPlayerId,name:data.playerName,playerConnected:playerNumConnected}

            console.log("New Player! " + data.playerName);
            console.log(playerNumConnected + " player has connect");

            socket.emit('I Login', playerID);

            socket.broadcast.emit('Other Player Connected', playerID);
        }

    });
    socket.on('LogOut', function (data){





    });

});