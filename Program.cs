using System.Net;

namespace SP_HW_11_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> urls = new List<string>()
            {
                "https://mystat.itstep.org/ua/main/homework/page/index",
                "https://www.twitch.tv/"
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Task[] tasks = new Task[urls.Count];
            for(int i = 0; i < urls.Count; i++)
            {
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    while(urls.Count > 0 &&  !cts.Token.IsCancellationRequested)
                    {
                        string url = null;
                        lock (urls)
                        {
                            if(urls.Count > 0)
                            {
                                url = urls[0];
                                urls.RemoveAt(0);
                            }
                        }
                        if(url != null)
                        {
                            try
                            {
                                Console.WriteLine("Downloading...");
                                WebClient webClient = new WebClient();
                                string fileName = Path.GetFileName(url);
                                webClient.DownloadFile(url, fileName);
                                Console.WriteLine($"File {fileName} was successfully downloaded.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error while downloading: {ex.Message}");
                            }
                        }
                    }
                }, cts.Token);

                Console.WriteLine("Press any key to cancel downloading...");
                Console.ReadKey();

                cts.Cancel();

                Console.ReadLine();
            }
        }
    }
}
