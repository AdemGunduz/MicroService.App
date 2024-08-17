# Online Oyun Market Sistemi

Bu proje, bir online oyun market sistemi yönetmek için tasarlanmış mikroservis tabanlı bir uygulamadır.
Üç ana mikroservis içermektedir: MarketService, InventoryService ve AccountService. Bu mikroservisler, Ocelot API Gateway tarafından yönlendirilir. ve RabbitMQ ile MassTransit kullanılarak asenkron mesajlaşma sağlanmaktadır.

## Mikroservisler

### MarketService
MarketService, market ile ilgili işlemleri yönetmekten sorumludur. Ürünlerin pazar fiyatlarını ve işlemleri yönetir. . Bu servis, RabbitMQ ve MassTransit kullanarak diğer servislerle iletişim kurar.

### InventoryService
InventoryService, oyuncuların envanterlerini yönetmekten sorumludur. Bu servis, MongoDB kullanarak envanter verilerini saklar ve işler.

### AccountService
AccountService, oyuncu hesaplarının yönetimini sağlar. MongoDB kullanır.


Bu proje, bir online oyun market sistemi oluşturmayı amaçlar. Aşağıdaki ana bileşenleri içerir:

- **Player :** Oyuncuların hesap bilgilerini içerir.
- **Item :** Oyun içi eşyaları temsil eder.
- **Inventory :** Oyuncuların sahip olduğu eşyaları yönetir.
- **ItemInventory :** Eşyaların envanter içindeki durumunu takip eder.
- **Market :** Oyuncuların eşyalarını alıp sattığı pazar yerini yönetir.
