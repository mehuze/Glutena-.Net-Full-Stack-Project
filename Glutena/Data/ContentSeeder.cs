using Glutena.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Data
{
    // Bilgi sayfalarının ilk içeriğini veritabanına ekler.
    // Sayfa zaten varsa hiçbir şeye dokunmaz (admin panelindeki değişiklikler korunur).
    public static class ContentSeeder
    {
        public static async Task SeedAsync(GlutenaDbContext context)
        {
            if (await context.ContentPages.AnyAsync(p => p.Slug == "yeni-tani"))
                return;

            var page = new ContentPage
            {
                Slug = "yeni-tani",
                Title = "Yeni Tanı Rehberi",
                Subtitle = "Çölyak tanısı mı aldınız? Panik yapmayın. Bu rehber, ilk günlerde aklınıza gelen soruları adım adım yanıtlıyor.",
                Icon = "🌱",
                Intro = """
                    <p>Çölyak, gluten adlı proteinin ince bağırsağa zarar verdiği bir hastalıktır. İyi haber şu: <strong>tedavisi ilaç değil, beslenmedir.</strong> Glutensiz beslenmeye özenle uyduğunuzda bağırsaklarınız zamanla iyileşir ve sağlıklı bir yaşam sürebilirsiniz.</p>
                    <p>İlk haftalar kafa karıştırıcı olabilir. Aşağıdaki soruları sırayla okuyabilir ya da merak ettiğinize doğrudan atlayabilirsiniz.</p>
                    """,
                Footnote = "Bu sayfadaki bilgiler genel bilgilendirme amaçlıdır. Doktorunuzun ve diyetisyeninizin önerilerinin yerini tutmaz.",
                IsActive = true
            };

            page.Sections.Add(new ContentSection
            {
                Order = 1,
                Icon = "🥗",
                Title = "Bundan sonra ne yiyeceğim?",
                ShortTitle = "Ne yiyeceğim?",
                Body = """
                    <p>Gluten <strong>buğday, arpa ve çavdarda</strong> bulunur. Bu tahıllardan yapılan ekmek, makarna, börek, kek ve benzeri her şeyden kaçınmanız gerekir. Ama korkmayın, yiyebileceğiniz çok şey var.</p>
                    <h3>Doğal olarak glutensiz olanlar</h3>
                    <ul>
                        <li>Et, tavuk, balık, yumurta</li>
                        <li>Süt, sade yoğurt, peynir</li>
                        <li>Tüm taze sebze ve meyveler</li>
                        <li>Mercimek, nohut, fasulye</li>
                        <li>Pirinç, mısır, patates</li>
                        <li>Karabuğday, kinoa</li>
                        <li>İşlenmemiş kuruyemişler</li>
                        <li>Zeytinyağı, tereyağı</li>
                    </ul>
                    <p><small>Bu yiyecekler doğal halleriyle glutensizdir. Hazır, paketli ya da soslu hallerinde gluten eklenmiş olabilir.</small></p>
                    <h3>Yulaf ne olacak?</h3>
                    <p>Yulaf doğası gereği gluten içermez, ama tarlada ve fabrikada çoğu zaman buğdayla karışır. Sadece <strong>"glutensiz" etiketli</strong> yulafı tercih edin. Bazı çölyak hastaları glutensiz yulafa da tepki verebilir. Diyetinize eklemeden önce doktorunuza ya da diyetisyeninize danışın.</p>
                    <div class="cp-note"><strong>İlk iş: bir diyetisyenle görüşün.</strong> Glutensiz beslenmede lif, demir ve bazı vitaminler eksik kalabilir. Diyetisyen size dengeli ve uygulanabilir bir beslenme planı hazırlar.</div>
                    <p><a class="cp-btn" href="/Product">Glutensiz ürünlere göz at</a></p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 2,
                Icon = "🔎",
                Title = "Gluten nerelerde gizlenir?",
                ShortTitle = "Gizli gluten",
                Body = """
                    <p>Ekmek ve makarnayı herkes bilir. Asıl dikkat edilmesi gereken, akla gelmeyen kaynaklardır:</p>
                    <ul>
                        <li><strong>Bulgur, irmik, kuskus, firik</strong> (hepsi buğdaydır)</li>
                        <li>Şehriye ve erişte</li>
                        <li>Köfte, panelenmiş ürünler, galeta unu</li>
                        <li>Hazır çorbalar ve bulyonlar</li>
                        <li>Un ile koyulaştırılmış soslar</li>
                        <li>Soya sosu ve bazı hazır soslar</li>
                        <li><strong>Malt, malt özütü</strong> ve bira</li>
                        <li>Hazır baharat karışımları</li>
                        <li>Gofret, bazı çikolata ve şekerlemeler</li>
                        <li>Hazır kek ve puding karışımları</li>
                        <li>Bazı şarküteri ürünleri</li>
                        <li>Buğday ürünlerinin kızartıldığı yağ</li>
                    </ul>
                    <div class="cp-note">İlaç ve vitamin takviyelerinin içeriğini de eczacınıza sorun. Nadiren de olsa yardımcı madde olarak buğday nişastası kullanılabilir.</div>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 3,
                Icon = "🏷️",
                Title = "Etikette neye bakmalıyım?",
                ShortTitle = "Etiket okuma",
                Body = """
                    <h3>"Glutensiz" ibaresi</h3>
                    <p>Türkiye'de bir üründe <strong>"glutensiz"</strong> yazabilmesi için gluten miktarının en fazla <strong>20 ppm</strong> (kilogramda 20 miligram) olması gerekir. Çölyak hastaları için güvenli kabul edilen sınır budur.</p>
                    <h3>Üstü çizili başak sembolü</h3>
                    <p>Paketin üzerindeki <strong>üstü çizili başak</strong> sembolü, ürünün glutensiz olduğunun bağımsız bir kuruluş tarafından kontrol edildiğini gösterir.</p>
                    <h3>İçindekiler listesi</h3>
                    <ul>
                        <li><strong>Buğday, arpa, çavdar, malt, irmik, bulgur</strong> gibi kelimeleri arayın.</li>
                        <li>Alerjenler içindekiler listesinde genellikle <strong>kalın</strong> ya da farklı yazıyla belirtilir.</li>
                        <li><strong>"Eser miktarda gluten içerebilir"</strong> uyarısı olan ürünlerden kaçının.</li>
                        <li>Markalar tariflerini değiştirebilir. Sık aldığınız ürünlerin etiketini de ara ara kontrol edin.</li>
                    </ul>
                    <div class="cp-warn"><strong>Açıkta satılan ürünlere dikkat.</strong> İstanbul'da yapılan bir araştırmada fırın ve pastanelerde "glutensiz" diye satılan ürünlerin yaklaşık dörtte birinde gluten miktarının sınırı aştığı bulundu. Paketli ve etiketli ürünler daha güvenlidir.</div>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 4,
                Icon = "🏠",
                Title = "Evde mutfağımı nasıl düzenlemeliyim?",
                ShortTitle = "Evde mutfak",
                Body = """
                    <p>Evde gluten yiyen başka kişiler varsa, çok küçük kırıntılar bile sorun yaratabilir. Buna <strong>çapraz bulaşma</strong> denir. Birkaç basit önlemle evde güvenle yaşayabilirsiniz:</p>
                    <ul>
                        <li><strong>Ayrı tost makinesi</strong> ya da ekmek kızartma makinesi kullanın.</li>
                        <li><strong>Tahta kesme tahtası ve tahta kaşıklar</strong> gözeneklidir, kırıntı tutar. Glutensiz yemekler için ayrı tutun.</li>
                        <li><strong>Tereyağı, reçel ve peynir kavanozlarını</strong> ayırın. Ekmek değen bıçak kavanoza girmesin.</li>
                        <li><strong>Ayrı süzgeç</strong> kullanın. Normal makarnayı süzdüğünüz süzgeçte glutensiz makarna süzmeyin.</li>
                        <li>Kızartma yağını ortak kullanmayın.</li>
                        <li>Buğday unuyla çalışırken un havaya uçar. O sırada glutensiz yemek hazırlamayın.</li>
                        <li>Dolapta glutensiz ürünlere <strong>ayrı ve üst bir raf</strong> ayırın, üstlerine kırıntı dökülmesin.</li>
                        <li>Aynı mutfakta pişiriyorsanız <strong>önce glutensiz</strong> yemeği hazırlayın.</li>
                    </ul>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 5,
                Icon = "🍽️",
                Title = "Restoranda ne sormalıyım?",
                ShortTitle = "Restoranda",
                Body = """
                    <p>Siparişten önce çölyak olduğunuzu açıkça söyleyin. Bunun bir tercih ya da diyet değil, <strong>sağlık sorunu</strong> olduğunu belirtin. Sonra şunları sorun:</p>
                    <ul>
                        <li>"Bu yemekte un, irmik, bulgur ya da hazır sos var mı?"</li>
                        <li>"Glutensiz yemekler ayrı tezgâhta ve ayrı tavada mı hazırlanıyor?"</li>
                        <li>"Patates kızartması, panelenmiş ürünlerle aynı yağda mı kızartılıyor?"</li>
                        <li>"Makarna ve pilav aynı suda mı haşlanıyor?"</li>
                        <li>"Izgara, ekmek ısıtılan yüzeyde mi yapılıyor?"</li>
                    </ul>
                    <div class="cp-note">Cevaplardan emin olamazsanız sade ızgara et, haşlanmış patates ve sossuz salata gibi basit yemekleri tercih edin.</div>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 6,
                Icon = "🤝",
                Title = "Bana kim yardım eder?",
                ShortTitle = "Kim yardım eder?",
                Body = """
                    <p>Yalnız değilsiniz. Bu yolda size destek olacak pek çok kişi ve kurum var:</p>
                    <ul>
                        <li><strong>Gastroenteroloji doktorunuz:</strong> Düzenli kontrollerinizi aksatmayın. Bağırsağınızın iyileşmesini o takip eder.</li>
                        <li><strong>Diyetisyen:</strong> Günlük beslenmenizi planlamanıza ve eksiklikleri önlemenize yardım eder.</li>
                        <li><strong>Çölyak dernekleri:</strong> Türkiye'de pek çok şehirde çölyak derneği var. Şehrinizdeki derneği araştırın. Etkinlikler, bilgilendirme ve deneyim paylaşımı için çok değerlidirler.</li>
                        <li><strong>Aileniz ve çevreniz:</strong> Hastalığınızı onlara anlatın. Ne kadar iyi anlarlarsa evde ve davetlerde o kadar rahat edersiniz.</li>
                        <li><strong>Glutena topluluğu:</strong> Ürünler hakkında diğer çölyak hastalarının deneyimlerini okuyun, sorularınızı sorun. Sorularınızı 🌿 Glutena olarak yanıtlıyoruz.</li>
                    </ul>
                    <p>
                        <a class="cp-btn" href="/Experience">Deneyimleri oku</a>
                        <a class="cp-btn cp-btn-outline" href="/Home/Contact">Bize soru sor</a>
                    </p>
                    """
            });

            context.ContentPages.Add(page);
            await context.SaveChangesAsync();
        }
    }
}