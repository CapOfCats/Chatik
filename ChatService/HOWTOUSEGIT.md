### Если репозитория вообще нет
git clone https://github.com/CapOfCats/Chatik.git

### Если есть локальный, хотите подключиться к общему репо
git remote add origin https://github.com/CapOfCats/Chatik.git
git branch -M main

### Получить все обновления общего репо себе
git pull

### Создать ветку
git branch branchName
### Перейти в существующую ветку
git checkout branchName
### Создать и перейти в ветку
git checkout -b branchName

### Сохранить версию
git add .
git commit -m "commit name"

### Отправить версию в общий репо
git push -u origin main # -u только в первый раз
