var port = process.env.PORT || 3000;
var io = require('socket.io')(port);

//Adding
/*var shortId = require('shortid');
var players = [];*/


var RandomNum = Math.floor(Math.random() * 100);

console.log("server started on port " + port);

console.log(RandomNum);

io.on("connection",function(socket){

    //Adding
    /*var thisPlayerId = shortId.generate();
    players[thisPlayerId] = player;
    console.log("client connected, id = ", thisPlayerId);
    socket.emit('register', {id:thisPlayerId});
    socket.broadcast.emit('spawn', {id:thisPlayerId});
    for(var playerId in players){
        if(playerId == thisPlayerId)
            continue;
        socket.emit('spawn', players[playerId]);
    };
    socket.on('disconnect', function () {
        console.log('client disconected');
        delete players[thisPlayerId];
        socket.broadcast.emit('disconnected', {id:thisPlayerId});
    });*/


    console.log("client connect");
    socket.emit("open");

    socket.on("Check",function(data){
        
        //Adding
        //data.id = thisPlayerId;
        //socket.broadcast.emit('playName',{id:thisPlayerId});

        //console.log(playerNum);
        
        //data = data.mynum;
        //delete data.mynum;
        
        if(data.mynum == RandomNum)
        {
            var result = {text:"Win"}
            RandomNum = Math.floor(Math.random() * 100);
            console.log(RandomNum);
            
        }
        else if(data.mynum < RandomNum)
        {
            var result = {text:"Less"}
        }
        else if(data.mynum > RandomNum)
        {
            var result = {text:"More"}
        }
        
        socket.emit("getValue",result);
        
        
    });

});