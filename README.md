# Online Oyun Market Sistemi

Bu proje, bir online oyun market sistemi yönetmek için tasarlanmış mikroservis tabanlı bir uygulamadır.
Üç ana mikroservis içermektedir: MarketService, InventoryService ve AccountService. Servisler arası iletişim için API Gateway kullanılmaktadır ve RabbitMQ ile MassTransit kullanılarak asenkron mesajlaşma sağlanmaktadır.

## Mikroservisler

### MarketService
MarketService, market ile ilgili işlemleri yönetmekten sorumludur. Oyuncuların eşyalarını alıp satmalarını sağlar. Bu servis, RabbitMQ ve MassTransit kullanarak diğer servislerle iletişim kurar.

### InventoryService
InventoryService, oyuncuların envanterlerini yönetmekten sorumludur. Bu servis, MongoDB kullanarak envanter verilerini saklar ve işler.

### AccountService
AccountService, oyuncu hesaplarının yönetimini sağlar. MongoDB kullanır.


Bu proje, bir online oyun market sistemi oluşturmayı amaçlar. Aşağıdaki ana bileşenleri içerir:

- **Player (Oyuncu):** Oyuncuların hesap bilgilerini içerir.
- **Item (Eşya):** Oyun içi eşyaları temsil eder.
- **Inventory (Envanter):** Oyuncuların sahip olduğu eşyaları yönetir.
- **ItemInventory (Eşya Envanteri):** Eşyaların envanter içindeki durumunu takip eder.
- **Market (Pazar):** Oyuncuların eşyalarını alıp sattığı pazar yerini yönetir.
