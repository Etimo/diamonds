#FROM python:3.7.0a1-alpine
FROM jfloff/alpine-python:3.4-slim

WORKDIR /app
ADD ./requirements.txt .

RUN pip install -r requirements.txt

ADD . .

ENTRYPOINT ["bash", "./auto_play_many.sh"]
