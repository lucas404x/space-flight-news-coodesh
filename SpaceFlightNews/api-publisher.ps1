$IMAGE_VERSION = '1.0'
$IMAGE_NAME = 'space-flight-news-api'

docker build -f Dockerfile -t $IMAGE_NAME`:v$IMAGE_VERSION .

docker run `
-dp 5000:80 `
--rm `
--name space-flight-news-instance `
-e DB_CONNECTION=$env:DB_CONNECTION `
$IMAGE_NAME`:v$IMAGE_VERSION
