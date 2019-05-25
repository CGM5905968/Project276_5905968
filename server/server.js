var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(port);
var shortId = require('shortid');
var port = process.env.PORT || 3000;

console.log("server started on port " + port);

var RandomNum = Math.floor(Math.random() * 100);
console.log(RandomNum);

var playerNumConnected;
var playerNumReady;

var isPlaying = false;


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