using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace TopicSender
{
    /// <summary>
    /// Send messages to a topic 
    /// Azure Topic and Subscriptions example
    /// </summary>
  
    class Program
    {
        const string ServiceBusConnectionString = "<Your Service Bus Connection String>";
        const string TopicName = "azdevtesttopic"; //name of your topic 
        static ITopicClient topicClient;


        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            const int numberOfMsgs = 10;
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            await SendMessagesAsync(numberOfMsgs);
            Console.ReadKey();
            await topicClient.CloseAsync();
        }


        static async Task SendMessagesAsync(int noOfMsgsToSend)
        {
            try
            {
                for (int i = 0; i < noOfMsgsToSend; i++)
                {
                    //Create new messages to send to the topic
                    string messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    //Print out msg to console 
                    Console.WriteLine($"Sending message: {messageBody}");

                    //Send msg to the topic
                    await topicClient.SendAsync(message);
                
                }
            }
            catch (Exception ex)
            {

               Console.WriteLine($"Error with exception: { ex.Message}");
            }
        }
    }
}
