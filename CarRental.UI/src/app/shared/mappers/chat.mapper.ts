import { IChat } from "../../models/chat.model";

export function mapChatApiToApp(chat: any): IChat {
  return {
    id: chat.Id,
    sender: chat.Sender,
    senderId: chat.SenderId,
    receiver: chat.Receiver,
    receiverId: chat.ReceiverId,
    message: chat.Message,
    read: chat.Read,
    timeStamp: chat.TimeStamp
  };
}

export function mapChatAppToApi(chat: IChat): any {
  return {
    Id: chat.id,
    Sender: chat.sender,
    SenderId: chat.senderId,
    Receiver: chat.receiver,
    ReceiverId: chat.receiverId,
    Message: chat.message,
    Read: chat.read,
    TimeStamp: chat.timeStamp
  };
}
