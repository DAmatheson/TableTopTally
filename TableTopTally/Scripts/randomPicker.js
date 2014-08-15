function randomName(namesId, outputId)
{
    var reNameSplit = /[,?\s*]+/; // 0-1 commas, any # spaces, one or more repetitions
    var reStartTrim = /^[,?\s*]+/; // At the start, 0-1 commas, any # spaces, one or more repetitions
    var reEndTrim = /[,?\s*]+$/; // At the end, 0-1 commas, any # spaces, one or more repetitions
    
    var names = document.getElementById(namesId).value;
    var trimmedNames = names.replace(reStartTrim, '').replace(reEndTrim, '');
    
    var splitNames = trimmedNames.split(reNameSplit);
    
    var randomNumber = getRandomInt(0, splitNames.length - 1);
    
    var pickedName = splitNames[randomNumber];
    
    document.getElementById(outputId).innerHTML = pickedName + " " + getTimestamp();
}

function getRandomInt(min, max)
{
    return Math.floor(Math.random() * (max - min + 1)) + min;   
}

function getTimestamp()
{
    var today = new Date();
    var day = today.getDate();
    var month = today.getMonth()+1; //January is 0!
    var year = today.getFullYear();
    var hours = today.getHours() > 12 ? today.getHours() - 12 : today.getHours();
    var minutes = today.getMinutes() >= 10 ? today.getMinutes() : "0" + today.getMinutes();
    var seconds = today.getSeconds() >= 10 ? today.getSeconds() : "0" + today.getSeconds();
    var timePeriod = today.getHours() > 12 ? "PM" : "AM";
    
    return day + "/" + month + "/" + year + " @ " + 
           hours + ":" + minutes + ":" + seconds + " " + timePeriod;
}