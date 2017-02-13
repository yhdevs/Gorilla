using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace TuvaletBekcisi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private const string TelegramToken = "376778207:AAGisAVWOWMrpEYZ6i-wqBNQuUXfRrJ8KvI";

        public List<User> _fakeUserData = new List<User>{
                    new User{
                        UserId = new Guid(),
                        SystemId = string.Empty,
                        UserName = "Selçuk"

                    },
                    new User{
                        UserId = new Guid(),
                        SystemId = string.Empty,
                        UserName = "Ümit"

                    },
                    new User{
                        UserId = new Guid(),
                        SystemId = string.Empty,
                        UserName = "Anıl"

                    },
                    new User{
                        UserId = new Guid(),
                        SystemId = string.Empty,
                        UserName = "Mennan"

                    },
                    new User{
                        UserId = new Guid(),
                        SystemId = string.Empty,
                        UserName = "Muhammed"

                    },

                    };

        public List<Request> _fakeRequestData = new List<Request>{
                    new Request{
                    RequestId = new Guid(),
                    RequestedDate = DateTime.Now.AddHours(-3),
                    UserId = new Guid(),
                    State = RequestStatus.Tamamlandı

                    },
                    new Request{
                    RequestId = new Guid(),
                    RequestedDate = DateTime.Now.AddHours(-2),
                    UserId = new Guid(),
                    State = RequestStatus.Tamamlandı

                    },new Request{
                    RequestId = new Guid(),
                    RequestedDate = DateTime.Now.AddHours(-1),
                    UserId = new Guid(),
                    State = RequestStatus.Beklemede

                    },new Request{
                    RequestId = new Guid(),
                    RequestedDate = DateTime.Now.AddMinutes(-10),
                    UserId = new Guid(),
                    State = RequestStatus.Beklemede

                    },new Request{
                    RequestId = new Guid(),
                    RequestedDate = DateTime.Now.AddSeconds(-20),
                    UserId = new Guid(),
                    State = RequestStatus.Beklemede

                    },

                    };

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<object> Post([FromBody]ApiAi value)
        {
            try
            {
                //botculardan gelen data
                if (value.status.code == 200)
                {
                    //linuxculara durum sorulacak
                    //boş
                    var state = 0;

                    //her gelen request'i db'ye kayıt at.

                    if (value.result.action == "tuvaletegitmekistiyorum") //UPDATE
                    {
                        var telegramId = value.result.contexts[0].parameters.telegram_chat_id;

                        //Tuvalet boşsa
                        if (state == 0)
                        {

                            SendTelegramMessage("Tuvalet boş, gidebilirsin", telegramId);

                            //request tamamlandı.

                        }
                        else
                        {
                            var reqCount = await TuvaleteGitmekIstiyorum(value);

                            if (reqCount == 0)
                            {
                                await SendTelegramMessage($"Tuvalet şuanda dolu, sıradaki kişi sizsiniz.", telegramId);
                            }
                            else
                            {
                                await SendTelegramMessage($"Sırada {reqCount} kişi var.", telegramId);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }


            return null;

        }

        private async Task<int> TuvaleteGitmekIstiyorum(ApiAi value)
        {
            int count = 0;
            try
            {
                //Db'den sıra durumu kontrol edilir. 
                //Tarihe göre sırayı ilk alan sıradaki.


                var user = _fakeUserData.FirstOrDefault(w => w.SystemId.Equals(value.result.contexts[0].parameters.telegram_chat_id));

                if (user == null)
                {
                    //Db'ye yeni user Kaydı

                }

                //Bekleyen kişi sayısını çekelim. 
                count = _fakeRequestData.Count(w => w.State == RequestStatus.Beklemede);
            }
            catch
            {

            }

            return count;
        }

        [HttpGet]
        public async Task<object> TuvaletBosaldi()
        {

            //veritabanından bekleyenler çekilecek

            var activeRequests = _fakeRequestData.Where(w => w.State == RequestStatus.Beklemede).OrderByDescending(w => w.RequestedDate).ToList();

            if (activeRequests.Count > 0)
            {
                var next = activeRequests.First();
                var nextUser = _fakeUserData.FirstOrDefault(w => w.UserId == next.UserId);

                if (nextUser != null)
                {
                    await SendTelegramMessage("Tuvalet boşalmıştır. Sıradaki kişi sizsiniz.", nextUser.SystemId);

                    //request status update edilecek.
                    next.State = RequestStatus.Bilgilendirildi;

                }





            };



            //await SendTelegramMessage();

            return null;


        }



        private async Task<bool> SendTelegramMessage(string message, dynamic telegramId)
        {
            using (var http = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new
                {
                    chat_id = telegramId,
                    text = message,
                    parse_mode = "HTML"
                });

                var result = await http.PostAsync($"https://api.telegram.org/bot{TelegramToken}/sendMessage", new StringContent(json, Encoding.UTF8, "application/json"));

            }

            return true;
        }

    }
}
