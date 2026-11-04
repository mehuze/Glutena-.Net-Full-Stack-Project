using Glutena.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Data
{
    // KVKK Aydınlatma Metni ve Gizlilik/Çerez Politikası sayfalarının taslaklarını ekler.
    // Sayfalar zaten varsa hiçbir şeye dokunmaz.
    public static class LegalPagesSeeder
    {
        public static async Task SeedAsync(GlutenaDbContext context)
        {
            if (!await context.ContentPages.AnyAsync(p => p.Slug == "kvkk-aydinlatma-metni"))
                context.ContentPages.Add(KvkkPage());

            if (!await context.ContentPages.AnyAsync(p => p.Slug == "gizlilik-ve-cerez-politikasi"))
                context.ContentPages.Add(PrivacyPage());

            await context.SaveChangesAsync();
        }

        // ================= KVKK AYDINLATMA METNİ =================
        private static ContentPage KvkkPage()
        {
            var page = new ContentPage
            {
                Slug = "kvkk-aydinlatma-metni",
                Title = "KVKK Aydınlatma Metni",
                Subtitle = "Kişisel verilerinizin nasıl işlendiği hakkında bilgilendirme",
                Icon = "⚖️",
                Intro = """
                    <p>Glutena olarak kişisel verilerinizin güvenliğine önem veriyoruz. Bu metin, 6698 sayılı Kişisel Verilerin Korunması Kanunu ("KVKK") kapsamında, sitemizi kullanırken hangi kişisel verilerinizin, hangi amaçlarla ve nasıl işlendiği konusunda sizi bilgilendirmek için hazırlanmıştır.</p>
                    """,
                Footnote = "Son güncelleme: [Tarih]",
                IsActive = true
            };

            page.Sections.Add(new ContentSection
            {
                Order = 1,
                Icon = "👤",
                Title = "Veri sorumlusu",
                ShortTitle = "Veri sorumlusu",
                Body = """
                    <p>Kişisel verileriniz, veri sorumlusu sıfatıyla <strong>[Ad Soyad]</strong> tarafından işlenmektedir.</p>
                    <p>İletişim: <a href="mailto:glutena.iletisim@gmail.com">glutena.iletisim@gmail.com</a></p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 2,
                Icon = "📋",
                Title = "Hangi kişisel verileri işliyoruz?",
                ShortTitle = "İşlenen veriler",
                Body = """
                    <ul>
                        <li><strong>Üyelik bilgileri:</strong> E-posta adresiniz ve şifreniz. Şifreniz okunamayacak şekilde şifrelenerek saklanır, biz dahil kimse göremez.</li>
                        <li><strong>Deneyim ve yorumlar:</strong> Ürünler hakkında yazdığınız başlık, yorum ve puan.</li>
                        <li><strong>İletişim formu:</strong> Adınız, e-posta adresiniz, seçtiğiniz konu ve mesajınız.</li>
                        <li><strong>Teknik bilgiler:</strong> Sitenin çalışması için gerekli çerezler. Barındırma hizmeti sağlayıcısı tarafından IP adresi ve erişim zamanı gibi sunucu kayıtları tutulabilir.</li>
                    </ul>
                    <div class="cp-warn"><strong>Sağlık bilgisi hakkında:</strong> Yorumlarınızda çölyak hastalığı gibi sağlık durumunuza ilişkin bilgiler paylaşabilirsiniz. Sağlık verileri KVKK'ya göre özel nitelikli kişisel veridir. Yorumlarınız sitede herkese açık yayınlandığı için, paylaşmak istemediğiniz sağlık bilgilerini yorumlarınıza yazmamanızı öneririz.</div>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 3,
                Icon = "🎯",
                Title = "Kişisel verilerinizi hangi amaçlarla işliyoruz?",
                ShortTitle = "Amaçlar",
                Body = """
                    <ul>
                        <li>Üyelik hesabınızı oluşturmak ve giriş yapmanızı sağlamak</li>
                        <li>Ürün deneyimlerinizi yayınlamak ve sorularınızı yanıtlamak</li>
                        <li>İletişim formu ile gönderdiğiniz mesajlara dönüş yapmak</li>
                        <li>Sitenin güvenliğini sağlamak ve kötüye kullanımı önlemek</li>
                        <li>Yasal yükümlülüklerimizi yerine getirmek</li>
                    </ul>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 4,
                Icon = "📑",
                Title = "Toplama yöntemi ve hukuki sebep",
                ShortTitle = "Hukuki sebep",
                Body = """
                    <p>Kişisel verileriniz, sitemizdeki üyelik, deneyim ve iletişim formlarını doldurmanız yoluyla elektronik ortamda toplanır.</p>
                    <p>Bu veriler KVKK'nın 5. maddesinde belirtilen şu hukuki sebeplere dayanılarak işlenir: bir sözleşmenin kurulması veya ifası için gerekli olması (üyelik), veri sorumlusunun meşru menfaati (sitenin güvenliği ve işleyişi) ve hukuki yükümlülüklerin yerine getirilmesi.</p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 5,
                Icon = "🔁",
                Title = "Kişisel verileriniz kimlere aktarılabilir?",
                ShortTitle = "Aktarım",
                Body = """
                    <p>Kişisel verileriniz satılmaz ve reklam amacıyla üçüncü kişilerle paylaşılmaz. Verileriniz yalnızca şu durumlarda aktarılabilir:</p>
                    <ul>
                        <li>Sitenin çalışması için hizmet aldığımız <strong>barındırma (hosting) ve e-posta hizmeti sağlayıcılarına</strong>, hizmetin gerektirdiği ölçüde</li>
                        <li>Yasal olarak yetkili <strong>kamu kurum ve kuruluşlarına</strong>, talep edilmesi halinde</li>
                    </ul>
                    <p>Yorumlarınız, deneyim paylaşma amacıyla sitede <strong>herkese açık</strong> olarak yayınlanır. E-posta adresiniz yorumlarınızla birlikte gösterilmez.</p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 6,
                Icon = "🗓️",
                Title = "Ne kadar süre saklıyoruz?",
                ShortTitle = "Saklama süresi",
                Body = """
                    <p>Üyelik bilgileriniz hesabınız açık olduğu sürece saklanır. Hesabınızın silinmesini istediğinizde üyelik bilgileriniz silinir. Yorumlarınız sitede kalmaya devam edebilir, ancak artık hesabınızla ilişkilendirilmez. Yorumlarınızın da silinmesini istiyorsanız bunu ayrıca talep edebilirsiniz.</p>
                    <p>İletişim formu mesajları, talebiniz sonuçlandıktan sonra makul bir süre saklanır ve ardından silinir.</p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 7,
                Icon = "✋",
                Title = "Haklarınız",
                ShortTitle = "Haklarınız",
                Body = """
                    <p>KVKK'nın 11. maddesi uyarınca şu haklara sahipsiniz:</p>
                    <ul>
                        <li>Kişisel verilerinizin işlenip işlenmediğini öğrenme</li>
                        <li>İşlenmişse buna ilişkin bilgi talep etme</li>
                        <li>İşlenme amacını ve amacına uygun kullanılıp kullanılmadığını öğrenme</li>
                        <li>Yurt içinde veya yurt dışında aktarıldığı üçüncü kişileri bilme</li>
                        <li>Eksik veya yanlış işlenmişse düzeltilmesini isteme</li>
                        <li>Kanunda öngörülen şartlar çerçevesinde silinmesini veya yok edilmesini isteme</li>
                        <li>Düzeltme ve silme işlemlerinin, verilerin aktarıldığı üçüncü kişilere bildirilmesini isteme</li>
                        <li>Otomatik sistemlerle analiz edilmesi sonucu aleyhinize bir sonuç çıkmasına itiraz etme</li>
                        <li>Kanuna aykırı işlenmesi sebebiyle zarara uğramanız halinde zararın giderilmesini talep etme</li>
                    </ul>
                    <div class="cp-note">Bu haklarınızı kullanmak için <a href="mailto:glutena.iletisim@gmail.com">glutena.iletisim@gmail.com</a> adresine yazabilirsiniz. Başvurunuz en geç 30 gün içinde ücretsiz olarak yanıtlanır.</div>
                    """
            });

            return page;
        }

        // ================= GİZLİLİK VE ÇEREZ POLİTİKASI =================
        private static ContentPage PrivacyPage()
        {
            var page = new ContentPage
            {
                Slug = "gizlilik-ve-cerez-politikasi",
                Title = "Gizlilik ve Çerez Politikası",
                Subtitle = "Sitemizde gizliliğinizi nasıl koruduğumuz ve hangi çerezleri kullandığımız",
                Icon = "🔒",
                Intro = """
                    <p>Bu sayfa, Glutena'yı kullanırken gizliliğinizi nasıl koruduğumuzu ve sitemizde hangi çerezlerin kullanıldığını açıklar. Kişisel verilerinizin işlenmesine ilişkin ayrıntılı bilgi için <a href="/sayfa/kvkk-aydinlatma-metni">KVKK Aydınlatma Metni</a>'ni inceleyebilirsiniz.</p>
                    """,
                Footnote = "Son güncelleme: [Tarih]",
                IsActive = true
            };

            page.Sections.Add(new ContentSection
            {
                Order = 1,
                Icon = "🍪",
                Title = "Çerez nedir?",
                ShortTitle = "Çerez nedir?",
                Body = """
                    <p>Çerezler, bir web sitesini ziyaret ettiğinizde tarayıcınıza kaydedilen küçük metin dosyalarıdır. Sitenin sizi hatırlamasını, örneğin giriş yaptığınızı bilmesini sağlarlar.</p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 2,
                Icon = "📋",
                Title = "Hangi çerezleri kullanıyoruz?",
                ShortTitle = "Kullanılan çerezler",
                Body = """
                    <p>Glutena yalnızca sitenin çalışması için <strong>zorunlu çerezleri</strong> kullanır:</p>
                    <ul>
                        <li><strong>Oturum çerezi:</strong> Giriş yaptığınızı hatırlar. Çıkış yaptığınızda ya da oturum süresi dolduğunda geçersiz olur.</li>
                        <li><strong>Güvenlik çerezi:</strong> Formların başka siteler tarafından kötüye kullanılmasını engeller.</li>
                        <li><strong>Bildirim çerezi:</strong> "Mesajınız alındı" gibi tek seferlik bildirimleri gösterir ve gösterildikten sonra silinir.</li>
                        <li><strong>Çerez tercihi:</strong> Çerez bildirimini kapattığınızı hatırlar, böylece her sayfada tekrar gösterilmez.</li>
                    </ul>
                    <div class="cp-note">Sitemizde <strong>reklam, pazarlama veya takip çerezi kullanılmaz.</strong> Ziyaretlerinizi izleyen üçüncü taraf analiz araçları bulunmaz.</div>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 3,
                Icon = "⚙️",
                Title = "Çerezleri nasıl yönetebilirim?",
                ShortTitle = "Çerez yönetimi",
                Body = """
                    <p>Tarayıcınızın ayarlarından çerezleri silebilir veya engelleyebilirsiniz. Ancak zorunlu çerezleri engellerseniz siteye giriş yapamayabilir ve formları gönderemeyebilirsiniz.</p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 4,
                Icon = "🛡️",
                Title = "Verilerinizi nasıl koruyoruz?",
                ShortTitle = "Güvenlik",
                Body = """
                    <ul>
                        <li>Sitemiz güvenli bağlantı (HTTPS) üzerinden çalışır. Gönderdiğiniz bilgiler şifrelenerek iletilir.</li>
                        <li>Şifreniz okunamayacak şekilde şifrelenerek saklanır.</li>
                        <li>Yönetim paneline yalnızca yetkili kişiler erişebilir.</li>
                        <li>Yorumlar yayınlanmadan önce incelenir.</li>
                    </ul>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 5,
                Icon = "🔗",
                Title = "Dış bağlantılar",
                ShortTitle = "Dış bağlantılar",
                Body = """
                    <p>Sitemizde başka sitelere, örneğin ürünlerin satıldığı mağazalara bağlantılar bulunabilir. Bu sitelerin gizlilik uygulamalarından Glutena sorumlu değildir. Ziyaret ettiğiniz sitelerin gizlilik politikalarını incelemenizi öneririz.</p>
                    """
            });

            page.Sections.Add(new ContentSection
            {
                Order = 6,
                Icon = "✉️",
                Title = "Bize ulaşın",
                ShortTitle = "İletişim",
                Body = """
                    <p>Gizlilik ile ilgili sorularınız için <a href="mailto:glutena.iletisim@gmail.com">glutena.iletisim@gmail.com</a> adresine yazabilir ya da <a href="/Home/Contact">iletişim formunu</a> kullanabilirsiniz.</p>
                    """
            });

            return page;
        }
    }
}