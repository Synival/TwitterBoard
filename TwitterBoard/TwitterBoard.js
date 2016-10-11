function postToSql (e) {
    // Get our relevant fields / divs and message.
    var $tweet   = $("#tweet-text");
    var $testDiv = $("#client-test-div");
    var message  = $tweet.val ();

    // Don't do anything for blank messages.
    if (message === "" || message == null)
        return false;

    // Can't post messages that are too long.
    if (message.length > 140) {
        alert ("That message is too long by " + (message.length - 140) + " characters!");
        return false;
    }

    // Update our div.
    $testDiv.html ("Posting: " + message);
    $tweet.val ("");

    // Build an object with necessary data.
    var data = new Object ();
    data.message = message;

    // Make SQL post.
    $.ajax ({
        type: "POST",
        url:  "Default.aspx/PostTweet",
        data: JSON.stringify (data),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',

        error: function (request, status, error) {
            $testDiv.html ("<p>HTTP Request Error:</p><pre>" + error + "</pre>");
        },
        success: function (result) {
            $testDiv.html ("<p>HTTP Request Success:</p><pre>" + result.d + "</pre>");
        },
    });

    // Don't post data.
    return false;
}

$(document).ready (function() {
    // Get 'messages' div start adding elements to it.
    // TODO: use jQuery
    var messagesDiv = document.getElementById ("messages");

    // Load our stream of messages.
    var source = new EventSource ("Messages.stream");
    source.onmessage = function (event) {
        var obj = JSON.parse (event.data);
        if (obj.Message != null) {
            var newItem = document.createElement ("div");
            newItem.className = "tweet-entry";
            newItem.innerHTML = "<span class=tweet-id>" + obj.ID + "</span><span class=tweet-user>" + obj.UserFrom + "</span>: " + obj.Message + "\n";
            messagesDiv.insertBefore (newItem, messagesDiv.childNodes[0]);
            while (messagesDiv.childNodes[25]) {
                var item = messagesDiv.childNodes[25];
                messagesDiv.removeChild (item);
            }
        }
    }
    source.error = function () {
        $("#client-test-div").html (arguments);
    }
});