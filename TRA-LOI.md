# Bài kiểm tra giữa kì in4211
## câu 1a. Bốn trụ cột OOP
- đóng gói (encapsulation): Che giấu dữ liệu và bảo vệ dữ liệu bên trong lớp 
- kế thừa (inheritance): lớp con kế thừa thuộc tính và phương thức của lớp cha 
- đa hình (Polymorphism): Các lớp khác nhau có thể thực hiện cùng một phương thức theo cách khác nhau
- trừu tượng(Abstraction): Ân chi tiết cài đặt và chỉ thể hiện những chức năng cần thiêt.
## Câu 1b. Đóng gói Member
2 vấn đề:
- thuộc tính id và email có puclic set nên bên ngoài có thể thay đổi tùy ý 
- Constructor public cho phép tạo đối tượng mà không kiểm tra email.
Cách khắc phục: 
- Sử dụng private constructor. 
- Sử dụng factory method Create() để kiểm tra email
## Câu 2a. Sơ đồ trạng thái Registration
mermaid
stateDiagram-v2
    [*] --> Pending : Create()
    Pending --> Confirmed : Confirm()
    Pending --> Cancelled : Cancel()
    Confirmed --> Cancelled : Cancel()
    Cancelled --> [*]
## Câu 3a. Ba nguyên lý SOLID bị vi phạm
- SRP (Single Responsibility Principle):
Dòng 6-10: EventService vừa kiểm tra dữ liệu, lưu dữ liệu vào cơ sở dữ liệu, ghi log và gửi thông báo. Lớp có quá nhiều trách nhiệm.
- OCP (Open/Closed Principle):
Dòng 11-14: Khi thêm một kênh thông báo mới phải sửa trực tiếp câu lệnh if-else trong EventService.
- DIP (Dependency Inversion Principle):
Dòng 8, 12, 14: EventService phụ thuộc trực tiếp vào các lớp cụ thể như SqlConnection, EmailSender và SmsSender thay vì phụ thuộc vào abstraction.
## Câu 5a. Clean Architecture
### 1. Thiết lập tham chiếu giữa các project
- EduHub.Application tham chiếu EduHub.Domain.
- EduHub.Infrastructure tham chiếu EduHub.Application và EduHub.Domain.
- EduHub.API tham chiếu EduHub.Application và EduHub.Infrastructure.
### 2. Sơ đồ bốn lớp và chiều phụ thuộc
```mermaid
flowchart TD
    API["EduHub.API"]
    APP["EduHub.Application"]
    INFRA["EduHub.Infrastructure"]
    DOMAIN["EduHub.Domain"]
    API --> APP
    API --> INFRA
    INFRA --> APP
    INFRA --> DOMAIN
    APP --> DOMAIN

### 3. Vị trí các thành phần
- Event: EduHub.Domain/Entities/Event.cs
- IEventRepository: EduHub.Application/Interfaces/IEventRepository.cs
- EventService: EduHub.Application/Services/EventService.cs
- InMemoryEventRepository: EduHub.Infrastructure/Repositories/InMemoryEventRepository.cs
- EventsController: EduHub.API/Controllers/EventsController.cs
## Câu 5b. Giải thích chiều phụ thuộc
Project EduHub.Domain chứa các thành phần nghiệp vụ cốt lõi của hệ thống nên không được tham chiếu tới EduHub.Infrastructure.
Lý do:
- Domain là lớp trung tâm, chứa các Entity và quy tắc nghiệp vụ.
- Infrastructure chứa các thành phần triển khai chi tiết như Repository và lưu trữ dữ liệu.
- Nếu Domain tham chiếu Infrastructure sẽ làm cho lớp nghiệp vụ phụ thuộc vào chi tiết triển khai.
- Điều này vi phạm Dependency Rule của Clean Architecture.
Vì vậy, các project bên ngoài phải phụ thuộc vào Domain, không phải ngược lại.
