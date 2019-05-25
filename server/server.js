var port = process.env.PORT || 3000;
var io = require('socket.io')(port);
var shortId = require('shortid');


console.log("server started on port " + port);

var RanNum = Math.floor(Math.random() * 100);
console.log(RanNum);

var playerNumConnected = 0;
var playerNumReady = 0;

var isPlaying = false;

var isWinning = false;

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
        
        if(data.played == false){
            playerNumConnected--;

        }else if(data.played == true){
            playerNumReady--;
            playerNumConnected--;
        } 




    });

    socket.on('Guess', function(data){

        playerNumReady++;

        console.log("Guess Here");

        if(data.GuessNum == RanNum){

            isWinning = true;



        }else if(data.GuessNum < RanNum&&!isWinning)
        {
            var result = {text:"Less"}
            console.log(data.playerName + "Guess" + result);
        }
        else if(data.GuessNum > RanNum&&!isWinning)
        {
            var result = {text:"More"}

            console.log(data.playerName + "Guess" + result);

        }

        playerHere = {count:playerNumReady,id:data.id,name:data.name,myGuess:data.GuessNum,result}
        console.log(playerHere);

        if(!isWinning){
            socket.emit("getValue",playerHere);

            socket.broadcast.emit("SomeOneGuess", playerHere);
            console.log("Some One Guess!");
        }


        if(playerNumReady == playerNumConnected&&!isWinning){
            socket.emit('show');
            socket.broadcast.emit('show');
            playerNumReady = 0;

            console.log("Round End!!");
        }
        if(isWinning){
            var anwser = RanNum;
            socket.emit("I Am Winner",playerHere, anwser);
            socket.broadcast.emit("I Lose",playerHere, anwser);
            isWinning = false;
            console.log("This Player " + playerHere.data.name + " Is The Winner");
            RanNum = Math.floor(Math.random() * 100);
            console.log(RanNum);
            playerNumReady = 0;


        }



    });



});