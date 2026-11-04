using Glutena.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Data
{
    // Kullanım Koşulları sayfasının taslağını ekler.
    // Sayfa zaten varsa hiçbir şeye dokunmaz.
    public static class TermsPageSeeder
    {
        public static async Task SeedAsync(GlutenaDbContext context)
        {
            if (await context.ContentPages.AnyAsync(p => p.Slug == "kullanim-kosullari"))
                return;

            var page = new ContentPage
            {
                Slug = "kullanim-kosullari",
                Title = "Kullanım Koşulları",
                Subtitle = "Glutena'yı kullanırken geçerli olan kurallar",
                Icon = "📜",
                Intro = """
                    <p>Glutena'yı kullanarak bu koşulları kabul etmiş olursunuz. Lütfen siteyi kullanmadan önce okuyun. Kişisel verilerinizle ilgili bilgiler için <a href="/sayfa/kvkk-aydinlatma-metni">KVKK Aydınlatma Metni</a>'ni inceleyebilirsiniz.</p>
                    """,
                Footnote = "Son güncelleme: [Tarih]",
                IsActive = true
            };

            page.Sections.Add(new ContentSection
            {
                Order = 1,
                Icon = "🌿",
                Title = "Glutena nedir?",
                ShortTitle = "Glutena nedir?",
                Body = """
                    <p>Glutena, çölyak hastaları ve gluten hassasiyeti olanlar için hazırlanmış bir bilgi ve deneyim paylaşım platformudur. Sitede glutensiz ürünler hakkında bilgiler, kullanıcı deneyimleri ve rehber içerikler yer alır.</p>
                    <p>Glutena bir satış sitesi değildir. Sitede ürün satışı yapılmaz.</p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 2,
                Icon = "⚕️",
                Title = "Sağlık uyarısı",
                ShortTitle = "Sağlık uyarısı",
                Body = """
                    <div class="cp-warn"><strong>Sitedeki bilgiler tıbbi tavsiye değildir.</strong> Tanı, tedavi ve beslenme planınız için mutlaka doktorunuza ve diyetisyeninize danışın.</div>
                    <ul>
                        <li>Ürün bilgileri, markaların açıkladığı bilgilere ve kullanıcı deneyimlerine dayanır. Markalar ürünlerinin içeriğini önceden haber vermeden değiştirebilir.</li>
                        <li><strong>Satın almadan önce ürün etiketini her zaman kendiniz kontrol edin.</strong></li>
                        <li>Kullanıcı yorumları kişisel deneyimlerdir. Herkes aynı ürüne aynı tepkiyi vermeyebilir.</li>
                        <li>Gluten hassasiyeti testi yalnızca fikir vermek içindir, tanı koymaz.</li>
                    </ul>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 3,
                Icon = "👤",
                Title = "Üyelik",
                ShortTitle = "Üyelik",
                Body = """
                    <ul>
                        <li>Üye olurken doğru bir e-posta adresi kullanmalısınız.</li>
                        <li>Hesabınızın ve şifrenizin güvenliğinden siz sorumlusunuz. Şifrenizi kimseyle paylaşmayın.</li>
                        <li>Hesabınızla yapılan tüm paylaşımlar size ait kabul edilir.</li>
                    </ul>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 4,
                Icon = "💬",
                Title = "Yorum ve deneyim kuralları",
                ShortTitle = "Yorum kuralları",
                Body = """
                    <p>Glutena'nın güvenilir bir topluluk olması için paylaşımlarınızda şu kurallara uymanızı rica ederiz:</p>
                    <h3>Paylaşabilirsiniz</h3>
                    <ul>
                        <li>Denediğiniz ürünlerle ilgili kendi deneyimlerinizi</li>
                        <li>Diğer kullanıcılara faydalı olacak ipuçlarını ve önerileri</li>
                        <li>Ürünlerle ilgili sorularınızı</li>
                    </ul>
                    <h3>Paylaşmayın</h3>
                    <ul>
                        <li>Hakaret, küfür, nefret söylemi veya kişileri hedef alan ifadeler</li>
                        <li>Reklam, tanıtım ya da başka sitelere yönlendiren bağlantılar</li>
                        <li>Gerçeğe aykırı veya yanıltıcı bilgiler</li>
                        <li>Kendinizin ya da başkalarının telefon, adres gibi kişisel bilgileri</li>
                        <li>Denemediğiniz ürünler hakkında uydurma yorumlar</li>
                    </ul>
                    <div class="cp-note">Tüm yorumlar yayınlanmadan önce incelenir. Glutena, bu kurallara uymayan yorumları yayınlamama veya kaldırma hakkına sahiptir.</div>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 5,
                Icon = "⚖️",
                Title = "Sorumluluk",
                ShortTitle = "Sorumluluk",
                Body = """
                    <ul>
                        <li><strong>Yorumların sorumluluğu, yorumu yazan kullanıcıya aittir.</strong> Yorumlar Glutena'nın görüşünü yansıtmaz.</li>
                        <li>Glutena, ürün bilgilerinin doğru ve güncel olması için özen gösterir, ancak tüm bilgilerin her zaman eksiksiz ve hatasız olduğunu garanti edemez.</li>
                        <li>Hatalı bir ürün bilgisi fark ederseniz <a href="/Home/Contact">iletişim formundan</a> "Hatalı ürün bilgisi bildirimi" konusunu seçerek bize bildirebilirsiniz.</li>
                    </ul>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 6,
                Icon = "🛒",
                Title = "Satın alma bağlantıları ve sponsorlu içerik",
                ShortTitle = "Sponsorlu içerik",
                Body = """
                    <ul>
                        <li>Bazı ürün sayfalarında, ürünün satıldığı mağazalara yönlendiren bağlantılar bulunabilir. Bu bağlantılar üzerinden yapılan alışverişlerden Glutena komisyon alabilir. Bu durum ürünün fiyatını değiştirmez.</li>
                        <li>Markalarla yapılan işbirlikleri sonucu öne çıkarılan ürünler <strong>"Sponsorlu Ürün"</strong> olarak açıkça belirtilir.</li>
                        <li>Sponsorlu olsun ya da olmasın, tüm ürünlerin gluten bilgileri aynı özenle kontrol edilir.</li>
                        <li>Alışverişleriniz yönlendirildiğiniz mağaza ile sizin aranızdadır. Satış, teslimat ve iade konularında ilgili mağazanın koşulları geçerlidir.</li>
                    </ul>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 7,
                Icon = "©️",
                Title = "Fikri haklar",
                ShortTitle = "Fikri haklar",
                Body = """
                    <p>Glutena'daki rehber metinleri, tasarım ve logo Glutena'ya aittir. İzin alınmadan kopyalanamaz ve başka yerlerde kullanılamaz.</p>
                    <p>Ürün adları, marka logoları ve ürün görselleri ilgili markalara aittir.</p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 8,
                Icon = "🚪",
                Title = "Hesabın kapatılması",
                ShortTitle = "Hesap kapatma",
                Body = """
                    <ul>
                        <li>Hesabınızı istediğiniz zaman kapatabilirsiniz. Bunun için <a href="mailto:glutena.iletisim@gmail.com">glutena.iletisim@gmail.com</a> adresine yazmanız yeterlidir.</li>
                        <li>Bu koşullara uymayan hesaplar uyarı yapılarak ya da doğrudan kapatılabilir.</li>
                    </ul>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 9,
                Icon = "🔄",
                Title = "Değişiklikler ve iletişim",
                ShortTitle = "İletişim",
                Body = """
                    <p>Glutena bu koşulları zaman zaman güncelleyebilir. Güncel koşullar her zaman bu sayfada yer alır.</p>
                    <p>Sorularınız için <a href="mailto:glutena.iletisim@gmail.com">glutena.iletisim@gmail.com</a> adresine yazabilir ya da <a href="/Home/Contact">iletişim formunu</a> kullanabilirsiniz.</p>
                    """
            });

            context.ContentPages.Add(page);
            await context.SaveChangesAsync();
        }
    }
}