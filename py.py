class Məhsul:
    def __init__(self, ad, alış_qiyməti, satış_qiyməti, miqdar):
        self.ad = ad
        self.alış_qiyməti = alış_qiyməti
        self.satış_qiyməti = satış_qiyməti
        self.miqdar = miqdar
        self.satış_sayı = 0  # Satış statistikası üçün

class MarketTətbiqi:
    def __init__(self):
        self.məhsullar = []
        self.balans = 1000.0
        self.admin_statusu = False

    def əsas_menyu_göstər(self):
        print("\nƏsas Menyu:")
        print("1. Məhsulları göstər")
        print("2. Məhsul əlavə et")
        print("3. Məhsul sat")
        print("4. Balansı yoxla")
        print("5. Satış statistikası")
        print("6. Ən çox satılan məhsul")
        print("7. Çıxış\n")

    def məhsulları_göstər(self):
        if not self.məhsullar:
            print("Heç bir məhsul mövcud deyil.")
            return
        print("Mövcud Məhsullar:")
        for məhsul in self.məhsullar:
            print(f"Ad: {məhsul.ad}, Alış Qiyməti: {məhsul.alış_qiyməti:.2f} AZN, "
                  f"Satış Qiyməti: {məhsul.satış_qiyməti:.2f} AZN, Miqdar: {məhsul.miqdar}")

    def məhsul_əlavə_et(self):
        ad = input("Məhsulun adını daxil edin: ")
        alış_qiyməti = self.sorğu_edici_float("Məhsulun alış qiymətini daxil edin: ")
        satış_qiyməti = self.sorğu_edici_float("Məhsulun satış qiymətini daxil edin: ")
        miqdar = self.sorğu_edici_int("Məhsulun miqdarını daxil edin: ")
        yeni_məhsul = Məhsul(ad, alış_qiyməti, satış_qiyməti, miqdar)
        self.məhsullar.append(yeni_məhsul)
        print(f"'{ad}' məhsulu uğurla əlavə edildi.")

    def məhsul_sat(self):
        if not self.məhsullar:
            print("Satmaq üçün məhsul yoxdur.")
            return

        self.məhsulları_göstər()
        məhsul_adı = input("Satmaq istədiyiniz məhsulun adını daxil edin: ")
        məhsul = next((m for m in self.məhsullar if m.ad.lower() == məhsul_adı.lower()), None)

        if not məhsul:
            print("Daxil etdiyiniz məhsul tapılmadı.")
            return

        satış_miqdarı = self.sorğu_edici_int("Satmaq istədiyiniz miqdarı daxil edin: ")
        if satış_miqdarı <= 0:
            print("Miqdar müsbət rəqəm olmalıdır.")
            return

        if satış_miqdarı > məhsul.miqdar:
            print(f"Anbarda kifayət qədər məhsul yoxdur. Mövcud miqdar: {məhsul.miqdar}")
            return

        satış_dəyəri = satış_miqdarı * məhsul.satış_qiyməti
        self.balans += satış_dəyəri
        məhsul.miqdar -= satış_miqdarı
        məhsul.satış_sayı += satış_miqdarı

        if məhsul.miqdar == 0:
            self.məhsullar.remove(məhsul)
            print(f"'{məhsul.ad}' məhsulu tam satıldı və anbar siyahısından çıxarıldı.")

        print(f"Satış tamamlandı: {məhsul.ad}, Miqdar: {satış_miqdarı}, "
              f"Ümumi dəyər: {satış_dəyəri:.2f} AZN, Yenilənmiş balans: {self.balans:.2f} AZN")

    def balansı_yoxla(self):
        print(f"Cari balans: {self.balans:.2f} AZN")

    def satış_statistikası(self):
        print("Satış Statistikası:")
        statistik = [m for m in self.məhsullar if m.satış_sayı > 0]
        if not statistik:
            print("Hələ ki, heç bir məhsul satılmayıb.")
        for məhsul in statistik:
            print(f"Ad: {məhsul.ad}, Satış Sayı: {məhsul.satış_sayı}")

    def ən_çox_satılan_məhsul(self):
        if not self.məhsullar:
            print("Satış üçün məhsul yoxdur.")
            return

        ən_çox_satılan = max(self.məhsullar, key=lambda m: m.satış_sayı, default=None)
        if ən_çox_satılan and ən_çox_satılan.satış_sayı > 0:
            print(f"Ən çox satılan məhsul: {ən_çox_satılan.ad}, Satış Sayı: {ən_çox_satılan.satış_sayı}")
        else:
            print("Hələ ki, heç bir məhsul satılmayıb.")

    @staticmethod
    def sorğu_edici_float(mesaj):
        while True:
            try:
                return float(input(mesaj))
            except ValueError:
                print("Düzgün bir rəqəm daxil edin.")

    @staticmethod
    def sorğu_edici_int(mesaj):
        while True:
            try:
                return int(input(mesaj))
            except ValueError:
                print("Düzgün bir tam rəqəm daxil edin.")

if __name__ == "__main__":
    app = MarketT tətbiqi()

    istifadəçi_tip i= input("Hesab növünü seçin (admin/satıcı): ").lower()
    app.admin_statusu = istifadəçi_tip i== "admin"

    davam_edilsin = True
    while davam_edilsin:
        app.əsas_menyu_göstər()
        seçim = input("Əməliyyat seçin: ")
        if seçim == "1":
            app.məhsulları_göstər()
        elif seçim == "2":
            if app.admin_statusu:
                app.məhsul_əlavə_et()
            else:
                print("Satıcı olaraq məhsul əlavə etməyə icazəniz yoxdur.")
        elif seçim == "3":
            app.məhsul_sat()
        elif seçim == "4":
            app.balansı_yoxla()
        elif seçim == "5":
            app.satış_statistikası()
        elif seçim == "6":
            app.ən_çox_satılan_məhsul()
        elif seçim == "7":
            print("Market Tətbiqindən istifadə etdiyiniz üçün təşəkkür edirik!")
            davam_edilsin = False
        else:
            print("Yanlış seçim. Yenidən cəhd edin.")
