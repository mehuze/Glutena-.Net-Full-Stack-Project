using Glutena.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Data
{
    // Çölyak Hastalığı sayfasının ilk içeriğini ekler.
    // Sayfa zaten varsa hiçbir şeye dokunmaz.
    public static class ColyakPageSeeder
    {
        public static async Task SeedAsync(GlutenaDbContext context)
        {
            if (await context.ContentPages.AnyAsync(p => p.Slug == "colyak"))
                return;

            var page = new ContentPage
            {
                Slug = "colyak",
                Title = "Çölyak Hastalığı",
                Subtitle = "Çölyak nedir, hangi belirtileri verir, tanı için hangi testlere bakılır? Temel bilgiler bir arada.",
                Icon = "🌾",
                Intro = """
                    <p>Bu sayfada çölyak hastalığının ne olduğunu, hangi belirtilerle ortaya çıkabileceğini ve tanı sürecinde hangi testlerin önemli olduğunu bulabilirsiniz. Belirtileriniz size tanıdık geliyorsa bir doktora başvurmanızı öneririz.</p>
                    """,
                Footnote = "Bu sayfadaki bilgiler genel bilgilendirme amaçlıdır. Tanı ve tedavi için mutlaka doktorunuza danışın.",
                IsActive = true
            };

            page.Sections.Add(new ContentSection
            {
                Order = 1,
                Icon = "🌾",
                Title = "Çölyak hastalığı nedir?",
                ShortTitle = "Çölyak nedir?",
                Body = """
                    <p>Çölyak hastalığı, buğday, arpa ve çavdarda bulunan bir protein olan <strong>gluten</strong> tüketildiğinde bağışıklık sisteminin ince bağırsağa zarar verdiği otoimmün bir hastalıktır. Dünya genelinde yaklaşık her 100 kişiden 1'ini etkiler, ancak vakaların büyük çoğunluğu tanısız kalmaktadır.</p>
                    <p>Gluten hassasiyeti ise çölyaktan farklıdır. Bağışıklık sistemi tepkisi olmaksızın gluten tüketiminin sindirim sistemi belirtilerine yol açtığı bir durumdur.</p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 2,
                Icon = "🔍",
                Title = "Belirtiler",
                ShortTitle = "Belirtiler",
                Body = """
                    <h3>Sindirim sistemi belirtileri</h3>
                    <ul>
                        <li>Karın şişkinliği ve gaz</li>
                        <li>İshal veya kabızlık</li>
                        <li>Karın ağrısı ve kramp</li>
                        <li>Bulantı ve kusma</li>
                        <li>Yağlı veya kötü kokulu dışkı</li>
                    </ul>
                    <h3>Genel belirtiler</h3>
                    <ul>
                        <li>Kronik yorgunluk ve halsizlik</li>
                        <li>Açıklanamayan kilo kaybı</li>
                        <li>Demir eksikliği anemisi</li>
                        <li>Kemik ve eklem ağrısı</li>
                        <li>Cilt döküntüsü (Dermatitis Herpetiformis)</li>
                        <li>Baş ağrısı ve beyin sisi</li>
                        <li>Depresyon ve anksiyete</li>
                        <li>Ağız yaraları (aft)</li>
                    </ul>
                    <div class="cp-warn"><strong>Dikkat:</strong> Çölyak hastalığı her zaman sindirim belirtisi vermez. Bazı kişilerde sadece yorgunluk, anemi veya cilt sorunları görülebilir.</div>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 3,
                Icon = "🩸",
                Title = "Kan tahlilinde nelere bakılmalı?",
                ShortTitle = "Kan tahlilleri",
                Body = """
                    <p>Çölyak şüphesi durumunda doktorunuz aşağıdaki testleri isteyebilir:</p>
                    <ul>
                        <li><strong>Anti-tTG IgA:</strong> Çölyak için en önemli tarama testi. Doku transglutaminaz antikorlarını ölçer.</li>
                        <li><strong>Anti-endomisyum antikoru (EMA):</strong> Çölyak tanısını desteklemek için kullanılan ikincil test.</li>
                        <li><strong>Total IgA:</strong> IgA eksikliği varsa Anti-tTG IgA sonucu yanıltıcı olabilir. Birlikte bakılmalıdır.</li>
                        <li><strong>Demir ve ferritin:</strong> Bağırsakta emilim sorunu varsa düşük çıkabilir. Anemi kontrolü için gereklidir.</li>
                        <li><strong>B12 ve folat:</strong> İnce bağırsak hasarında emilim azalır. Sinir sistemi sağlığı için önemlidir.</li>
                        <li><strong>D vitamini:</strong> Çölyak hastalarında sık görülen bir eksikliktir. Kemik sağlığı için önemlidir.</li>
                    </ul>
                    <div class="cp-warn"><strong>Önemli:</strong> Kan testi yaptırmadan önce glutensiz diyete geçmeyin. Gluten yemiyorsanız test sonuçları negatif çıkabilir ve tanı kaçabilir.</div>
                    <div class="cp-note">Kan testleri yol gösterir. Kesin tanı çoğunlukla ince bağırsaktan alınan biyopsi ile konur. Hangi testlerin gerektiğine doktorunuz karar verir.</div>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 4,
                Icon = "⏰",
                Title = "Erken tanının önemi",
                ShortTitle = "Erken tanı",
                Body = """
                    <p>Çölyak hastalığı tanısız kaldığında uzun vadede ciddi sorunlara yol açabilir:</p>
                    <ul>
                        <li><strong>🦴 Osteoporoz:</strong> Kalsiyum emilim bozukluğu kemik erimesine yol açabilir.</li>
                        <li><strong>🧠 Nörolojik sorunlar:</strong> Sinir sistemi hasarı ve beyin fonksiyonlarında bozulma görülebilir.</li>
                        <li><strong>🤰 Üreme sağlığı:</strong> Kısırlık ve tekrarlayan düşük riskleri artabilir.</li>
                    </ul>
                    <p>Erken tanı ve glutensiz beslenme ile bu risklerin büyük kısmı azaltılabilir.</p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 5,
                Icon = "👣",
                Title = "Sonraki adımlar",
                ShortTitle = "Sonraki adımlar",
                Body = """
                    <p>Belirtileriniz size tanıdık geliyorsa önce kısa testimizi deneyebilir, ardından bir doktora başvurabilirsiniz. Tanı aldıysanız Yeni Tanı Rehberi'miz sizi adım adım yönlendirir.</p>
                    <p>
                        <a class="cp-btn" href="/Home/Test">Gluten testini başlat</a>
                        <a class="cp-btn cp-btn-outline" href="/sayfa/yeni-tani">Yeni Tanı Rehberi</a>
                        <a class="cp-btn cp-btn-outline" href="/Product">Glutensiz ürünler</a>
                    </p>
                    """
            });

            context.ContentPages.Add(page);
            await context.SaveChangesAsync();
        }
    }
}