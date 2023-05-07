using System.Text.RegularExpressions;

namespace PresentationLayer.Models
{
    public class ItemDetail
    {
        public string Id { get; set; }
        public string Amaunt { get; set; }
        public string Price { get; set; }
        
        public List<ItemDetail> ConvertToList(string s)
        {
            var list = new List<ItemDetail>();
            Regex re = new Regex("(?<id>[0-9]+)-(?<amaunt>[0-9.]+)-(?<price>[0-9.]+)");

            try
            {
                foreach (Match match in re.Matches(s))
                {
                    var item = new ItemDetail();
                    item.Id = match.Groups[1].Value;
                    item.Amaunt = match.Groups[2].Value;
                    item.Price = match.Groups[3].Value;

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                //nista za sad
            }
            return list;
        }
    }
}
