FROM python:3.7.0a1-alpine

WORKDIR /app
ADD ./requirements.txt .

RUN pip install -r requirements.txt

ADD . .

ENTRYPOINT ["sh", "./auto_play_many.sh"]
