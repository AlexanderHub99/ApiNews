# ApiNews
Нужно написать REST API агрегатор новостей.Пользователь подает на вход адрес новостного сайта или его RSS-ленты База данных агрегатора начинает автоматически пополняться новостями с этого сайта.У пользователя есть возможность просматривать список новостей из базы и искать их по подстроке в заголовке новости.В качестве примера требуется подключить 2 любых новостных сайта на выбор.Результат - исходный код агрегатора, а также рабочие адреса.Язык C# .Net. Хранилище - любая реляционная база

Для теста:
Сайт с которого брались Rss key (https://www.edu.ru/news/export/).
База данных создаться автоматически и при запуске приложения использован Entity Framework Core.Строка подключения лежит в (appsettings.json).
1) Запустите приложение.
2) В терминале:
       2.1) httprepl https://localhost:7073/api/News.
       2.2) connect https://localhost:7073/api/News.
       2.3) post -h Content-Type=application/json -c "{"Rss":["RSS KEY"]}".  Принимает массив Rss keys.
       2.4) get -h Content-Type=application/json Для получения сохранённых новостей из базы данных.
       2.5) get -h Content-Type=application/json Substring. Для получения новостей отфильтрованных по ключевому слову в titel новости(Принимает как знаки так и строки).
            Пустой Substring приведет к выводу всех новостей так как обработается как Get запрос на получение всех новостей.
       2.6)  connect https://localhost:7073/api/News/IdGetNews/
          2.6.1)  get -h Content-Type=application/json/ ID(Цифра)-Новости которую хотите посмотреть.
       2.7) connect https://localhost:7073/api/News/ID-НОВОСТИ КОТОРУЮ ХОТИТЕ ОТРЕДАКТИРОВАТЬ.
          2.7.1)  put -h Content-Type=application/json -c "{"id": id-НОВОСТИ,"title": "Изменил","description":"Изменил","link":"Изменил","lastBuildDate":"Изменил"}".
       2.8) connect https://localhost:7073/api/News/ID-НОВОСТИ КОТОРУЮ ХОТИТЕ УДАЛИТЬ.
         2.8.1)  delete -h Content-Type=application/json.

