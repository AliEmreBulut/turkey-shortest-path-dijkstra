# 🗺️ Route Optimizer: Shortest Path Algorithm

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

Bu proje, Türkiye'deki 81 il ve İzmir'in 30 ilçesi arasındaki en kısa mesafeleri hesaplamak için graf (graph) veri yapıları ve yönlendirme algoritmaları kullanılarak geliştirilmiş bir C# konsol uygulamasıdır. Algoritma, şehirler ve ilçeler arası komşuluk ilişkilerini baz alarak bir başlangıç noktasından diğer tüm hedeflere olan en düşük maliyetli (en kısa) güzergahı bulur.

## 🚀 Temel Özellikler

* **Graf Tabanlı En Kısa Yol Algoritması:** Düğümlerin (şehirler/ilçeler) ve kenarların (aralarındaki mesafeler) 2 boyutlu matrisler üzerinde modellendiği özelleştirilmiş bir arama algoritması.
* **Dinamik Veri İçe Aktarımı:** `ilmesafe.txt`, `illerin_komsulari.txt`, `ilce_mesafe.txt` ve `ilce_komsu.txt` gibi harici metin dosyalarından komşuluk (adjacency) ve uzaklık bilgilerinin parse edilerek sisteme yüklenmesi.
* **Detaylı Rota Analizi:** Yalnızca mesafeyi hesaplamakla kalmaz, aynı zamanda gidilecek güzergahı (örn: Şehir A - Şehir B - Şehir C) adım adım string formatında çıkarır.
* **Gerçek ve Teorik Mesafe Karşılaştırması:** Veritabanındaki teorik mesafeler ile komşuluklar üzerinden hesaplanan pratik en kısa yollar arasındaki sapmaların (maksimum ve minimum farklar) otomatik analizi.

## ⚙️ Nasıl Çalışır?



Sistem, haritadaki lokasyonları birer düğüm (node), aralarındaki mesafeleri ise ağırlıklı kenarlar (weighted edges) olarak kabul eden bir yaklaşım izler:

1. **Matrislerin Oluşturulması:** `.txt` dosyalarından okunan verilerle tüm şehirler (`All_city_array`) ve ilçeler (`county_array`) için uzaklık matrisleri oluşturulur. Birbiriyle komşu olmayan lokasyonların aralarındaki mesafe algoritma gereği "sonsuz" (`10000000`) olarak atanır.
2. **Rota Keşfi:** `Find_shortest_path_city` ve `find_shortest_path` metotları çalıştırılır. Sistem tüm düğümleri tarar, ziyaret edilmemiş en yakın düğümü seçer ve komşuları üzerinden geçerek ulaşılan yeni toplam mesafeleri mevcut mesafelerle kıyaslayarak günceller.
3. **Raporlama:** Elde edilen nihai güzergahlar ve sapma oranları konsola tablo formatında yazdırılır.

## 💻 Kurulum ve Çalıştırma

1. Repoyu klonlayın:
   ```bash
   git clone [https://github.com/AliEmreBulut/](https://github.com/AliEmreBulut/)[turkey-shortest-path-dijkstra].git
