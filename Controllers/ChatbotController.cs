using Microsoft.AspNetCore.Mvc;

namespace TMDT.Controllers
{
    public class ChatbotController : Controller
    {
        [HttpPost]
        public IActionResult Chat( string message)
        {
            // Process the message and generate a bot response (you can use AI services, databases, etc.)
            // For simplicity, a basic response is returned here
            string response = GenerateBotResponse(message);
            return Json(response);
        }

        private string GenerateBotResponse(string message)
        {
            // Convert message to lowercase for case-insensitive matching
            message = message.ToLower();

            if (message.Contains("hello") || message.Contains("xin") || message.Contains("ban") || message.Contains("chao") || message.Contains("hi"))
            {
                return "Hi there! How can I help you?";
            }
            else if (message.Contains("product") || message.Contains("sản") || message.Contains("phẩm"))
            {
                return "Please visit our product page for more information.";
            }
            else
            {
                // Generate a random response for unrecognized messages
                var random = new Random();
                int randomNumber = random.Next(1, 3); // Adjust the range based on the number of random responses

                switch (randomNumber)
                {
                    case 1:
                        return "I'm sorry, I didn't quite understand that.";
                    case 2:
                        return "Lỗi của bạn chưa rõ, hãy gửi thông tin cho Admin qua email: speakerhub@gmail.com";
                    default:
                        return "Lỗi của bạn chưa rõ, hãy gửi thông tin cho Admin qua email: speakerhub2@gmail.com";
                }
            }
        }
    }
}
