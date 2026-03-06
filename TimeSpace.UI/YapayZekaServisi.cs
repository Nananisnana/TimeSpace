using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace TimeSpace.UI
{
    public class YapayZekaServisi
    {
        
        private const string API_KEY = "BURAYA_API_ANAHTARI_GELECEK";

        public async Task<OyunSahnesi> HikayeGetir(string prompt, string donem)
        {
            try
            {
                
                var options = new RestClientOptions("https://api.openai.com")
                {
                    
                };
                var client = new RestClient(options);

                // İstek adresinin devamı (Endpoint)
                var request = new RestRequest("/v1/chat/completions", Method.Post);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"Bearer {API_KEY}");

                
                string sistemMesaji = "Sen 'TimeSpace' oyununun sunucususun. " +
                                      "GÖREV: Kullanıcının verdiği döneme ve karaktere uygun sürükleyici bir hikaye yaz. " +
                                      "ÖNEMLİ KURAL: Cevabın SADECE aşağıdaki JSON formatında olmalı. " +
                                      "DİKKAT: 'Hikaye' alanı tek parça düz metin olmalıdır. Asla içine başka { } açma. " +
                                      "JSON Şablonu: " +
                                      "{ \"Hikaye\": \"Buraya uzun hikaye metni gelecek...\", \"Secenek1\": \"Hamle A\", \"Secenek2\": \"Hamle B\", \"Secenek3\": \"Hamle C\", \"GorselTarifi\": \"...\" }";

                var body = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                        new { role = "system", content = sistemMesaji },
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.7
                };

                request.AddJsonBody(body);

                //  İSTEĞİ GÖNDER
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                    string gelenIcerik = jsonResponse.choices[0].message.content.ToString();

                    
                    gelenIcerik = gelenIcerik.Replace("```json", "").Replace("```", "").Trim();

                    if (gelenIcerik.StartsWith("json")) gelenIcerik = gelenIcerik.Substring(4).Trim();

                    // ÇEVİRME İŞLEMİ
                    try
                    {
                        OyunSahnesi sahne = JsonConvert.DeserializeObject<OyunSahnesi>(gelenIcerik);
                        return sahne;
                    }
                    catch (Exception)
                    {
                        
                        MessageBox.Show("AI Cevap Formatı Hatası:\n" + gelenIcerik);
                        return new OyunSahnesi { Hikaye = "Hikaye yüklenirken format hatası oluştu.", Secenek1 = "Tekrar Dene", Secenek2 = "", Secenek3 = "" };
                    }
                }
                else
                {
                    
                    MessageBox.Show($"Sunucu Hatası ({response.StatusCode}): {response.Content}");
                    return new OyunSahnesi { Hikaye = "Sunucuya ulaşılamadı.", Secenek1 = "Çıkış", Secenek2 = "", Secenek3 = "" };
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Kritik Hata: " + ex.Message);
                return new OyunSahnesi { Hikaye = "Bağlantı hatası.", Secenek1 = "Çıkış", Secenek2 = "", Secenek3 = "" };
            }
        }
    }
}