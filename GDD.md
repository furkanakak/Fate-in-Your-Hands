# Fate in Your Hands - Game Design Document

## 1. Oyun Ozeti

Fate in Your Hands, Choice of Life tarzinda kart tabanli bir secim oyunudur. Oyuncu kisa hikaye kartlari okur, iki secenekten birini secer ve secimin sonucunda yeni karta, can kaybina, olum kartina veya finale ilerler.

Oyun karmasik puan sistemleri uzerine kurulmaz. Oynanisin tek ana kaynagi candir.

Temel fikir:

- Oyuncu 3 canla baslar.
- Her kart kisa bir sahne anlatir.
- Her karar kartinda 2 secenek bulunur.
- Secimler dogrudan yeni karta, can degisimine, olum kartina veya final kartina baglanir.
- Oyuncu farkli sonlari, olumleri ve unvanlari acmak icin tekrar oynar.

## 2. Tasarim Hedefi

Oyuncunun her ekranda su soruyu sormasi hedeflenir:

"Bunu secersem hayatta kalir miyim?"

Oyun hizli, okunabilir ve tekrar oynanabilir olmalidir. Oyuncu uzun menulerle veya karmasik hesaplarla ugrasmaz. Baski, sadece can sayisi ve secimlerin geri donusu ile verilir.

## 3. Oynanis Ilkeleri

Zorunlu ilkeler:

- Tek ana kaynak candir.
- Varsayilan baslangic cani 3'tur.
- Her karar kartinda 2 secenek vardir.
- Her secenek sonucu net bir sonraki karta baglanir.
- Can 0 olursa olum karti acilir.
- Bazi secimler can bitmeden de dogrudan olum kartina gidebilir.
- Her hikaye birden fazla finale sahip olur.
- Her hikaye tekrar oynanabilir olacak sekilde kurulur.

Oyuncuya ayni anda sadece su bilgiler gosterilir:

- hikaye adi
- mevcut kart basligi
- can gostergesi
- hikaye metni
- iki secim

## 4. Olmayacak Sistemler

Oyunda ek oynanis puani, ekonomi, sosyal seviye, karakter puani veya gizli hesap mekanigi bulunmaz.

Hikaye farkliligi, arka planda puan biriktirerek degil; seceneklerin dogrudan baska kartlara baglanmasiyla uretilir.

## 5. Ana Oyun Dongusu

1. Oyuncu hikaye secer.
2. Baslangic karti acilir.
3. Oyuncu metni okur.
4. Oyuncu iki secenekten birini secer.
5. Secim sonucu uygulanir:
   - yeni karta gecis
   - can azalmasi
   - can artmasi
   - olum karti
   - final karti
   - unvan acilmasi
6. Oyuncu finale ulasir veya hayatini kaybeder.
7. Oyuncu tekrar deneyerek farkli rotalar bulur.

## 6. Can Sistemi

Baslangic cani:

```yaml
player:
  can: 3
```

Can etkileri:

- `canDelta: 0` can degismez.
- `canDelta: -1` can 1 azalir.
- `canDelta: 1` can 1 artar.

Kurallar:

- Can 0 olursa oyuncu olum kartina gider.
- Can artisi nadir kullanilir.
- Can artisi oyuncuya nefes aldirmali, oyunu kolaylastirmamalidir.
- Can gostergesi her zaman ekranda gorunur.

## 7. Kart Turleri

### Hikaye Karti

Oyuncuya sahne anlatir ve iki secim sunar.

### Olum Karti

Oyuncunun nasil hayatini kaybettigini kisa ve etkili anlatir.

### Final Karti

Hikayenin tamamlandigi noktadir. Oyuncunun ulastigi sonucu anlatir.

### Unvan Karti

Oyuncuya acilan basarimi bildirir. Unvanlar oynanisi degistirmez; sadece koleksiyon ve hedef hissi verir.

## 8. Kart Veri Formati

Ornek hikaye karti:

```yaml
card:
  cardId: ybh_001
  type: choice
  title: "Otogar"
  text: "Gece yarisi otogarda uyandin. Hava soguk. Karsida isigi yanan eski bir lokanta var."
  choices:
    - label: "Lokantaya git"
      canDelta: 0
      nextCardId: ybh_002
    - label: "Durakta kal"
      canDelta: -1
      nextCardId: ybh_003
```

Ornek olum karti:

```yaml
card:
  cardId: ybh_death_001
  type: death
  title: "Sogukta Uyku"
  text: "Sabaha kadar beklemenin guvenli oldugunu sandin. Soguk senden daha sabirliydi."
  unlockTitle: "Ilk Olum"
```

Ornek final karti:

```yaml
card:
  cardId: ybh_ending_001
  type: ending
  title: "Yeni Sabah"
  text: "Sabaha ulastin. Sehir hala yabanci, ama artik tamamen kaybolmus degilsin."
  unlockTitle: "Hayatta Kalan"
```

## 9. Secim Tasarimi

Her secim:

- kisa yazilmalidir,
- net bir eylem icermelidir,
- sonucu tamamen belli etmemelidir,
- en fazla bir can etkisi tasimalidir,
- dogrudan bir sonraki karta baglanmalidir.

Iyi secim metni:

- "Kapiyi ac"
- "Beklemeyi sec"
- "Merdivenden in"

Zayif secim metni:

- "Guvenli secenegi sec"
- "Kotu sonucu doguracak hareketi yap"
- "Can kaybetmeyecegin yolu izle"

## 10. Olum Tasarimi

Olumler oyunun cezasi degil, kesif parcasi olmalidir.

Olum kartlari:

- kisa olur,
- nedeni hissettirir,
- tekrar deneme istegi verir,
- bazen trajik, bazen absurt olabilir,
- oyuncuya hangi secimin bedel odettigini sezdirir.

Ilk prototip hedefi:

- en az 10 olum karti
- en az 5 final karti
- en az 8 unvan

## 11. Coklu Son Sistemi

Her hikaye en az 5 finale sahip olmalidir.

Final turleri:

- iyi final
- kotu final
- trajik final
- absurt final
- gizli final

Final, sadece "kazandin" veya "kaybettin" dememelidir. Oyuncunun sectigi rota final metninde hissedilmelidir.

## 12. Tekrar Deneme

Oyuncu hayatini kaybettiginde sunlar sunulur:

- son secime don
- hikaye basina don
- ana menuye don

Tekrar deneme sistemi oyunu hizli tutar. Oyuncu her olumden sonra ayni hikayeyi farkli secimlerle deneyebilir.

## 13. Kayit Sistemi

Kaydedilecek alanlar:

```yaml
save:
  schemaVersion: 1
  storyId: yeni_bir_hayat
  currentCardId: ybh_001
  can: 3
  checkpointCardId: ybh_001
  seenCards:
    - ybh_001
  seenDeaths:
    - ybh_death_001
  seenEndings:
    - ybh_ending_001
  unlockedTitles:
    - ilk_olum
  updatedAt: "2026-06-18T22:45:00+03:00"
```

Kayit sistemi baska oynanis puani saklamaz.

## 14. Ilk Prototip

Ilk prototip tek hikaye ile cikmalidir.

Hikaye: Yeni Bir Hayat

Kapsam:

- 45-60 kart
- 3 can
- 2 secenekli karar kartlari
- en az 10 olum
- en az 5 final
- en az 8 unvan
- son secime don
- sonlar galerisi
- olumler galerisi

## 15. Yeni Bir Hayat - Kisa Konsept

Oyuncu gece otogarda uyanir. Telefonu kapanmistir, hava soguktur ve sehir tamamen yabancidir. Tek amac sabaha ulasmaktir.

Baslangic rotalari:

- otogarda kalmak
- isikli lokantaya gitmek
- arka sokaktan sehre inmek

Olasi finaller:

- Yeni Sabah
- Son Durak
- Kayip Yolcu
- Yanlis Masa
- Sabah Treni

## 16. UI Kurallari

Oyun ekrani:

- ustte can gostergesi
- ortada kart gorseli
- kart altinda hikaye metni
- en altta iki secim butonu

Olum ekrani:

- olum basligi
- kisa olum metni
- son secime don
- hikaye basina don
- ana menu

Final ekrani:

- final adi
- final metni
- acilan unvan
- tekrar dene
- galeriye git

## 17. Basari Kriterleri

Oyun basarili sayilirsa:

- oyuncu ilk 1 dakikada kurali anlar,
- can sistemi gerilim yaratir,
- her secim merak uyandirir,
- olumler tekrar oynama istegi verir,
- en az 5 final aranacak kadar ilgi cekicidir,
- arayuz can ve secim disinda kalabalik bilgi tasimaz.

## 18. Sonraki Adim

Bir sonraki is, "Yeni Bir Hayat" icin 45-60 kartlik akisi cikarmaktir.

Her kart icin sunlar yazilmalidir:

- cardId
- type
- title
- text
- choices
- canDelta
- nextCardId
- unlockTitle
