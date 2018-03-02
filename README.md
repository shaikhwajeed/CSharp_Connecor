## Generating a deep link to your conversation from a Bot

The format for a deep link to the conversation that you can use in a bot is as follows:

`https://teams.microsoft.com/l/message/<channelId>/<messageId>?tenantId=<tenantId>`

The query parameters are:

* `channelId`&emsp;The channel ID which we can get from the ChannelData; for example, `19:cbe3683f25094106b826c9cada3afbe0@thread.skype`
* `messageId`&emsp;The message ID for the conversation; for example, `1519710874677`
* `tenantId`&emsp;The tenant ID which we can get from the ChannelData; for example, `fe4a8eba-2a31-4737-8e33-e5fae6fee194`
