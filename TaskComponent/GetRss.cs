using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Web.Syndication;

namespace TaskComponent
{
    public sealed class GetRss : IBackgroundTask
    {
        public string RssFeed { get; private set; }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            //Read async rss data
            await GetRssFeed();

            deferral.Complete();
        }

        private async Task GetRssFeed()
        {
            Uri newsFeed = new Uri("http://www.ynet.co.il/Integration/StoryRss550.xml");
            SyndicationClient rssClient = new SyndicationClient();
            var feed = await rssClient.RetrieveFeedAsync(newsFeed);

            RssFeed = feed.Items.FirstOrDefault().ToString();
        }
    }
}
