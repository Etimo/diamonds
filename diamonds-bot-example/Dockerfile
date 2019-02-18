FROM python:3.7.2-alpine
RUN apk add bash
RUN pip install pipenv

WORKDIR /app
ADD ./Pipfile .
ADD ./Pipfile.lock .

RUN pipenv sync

ADD . .

ENTRYPOINT ["pipenv", "run", "/app/auto_play_many.sh"]
