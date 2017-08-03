FROM node:8.2.1-alpine

RUN mkdir -p /build
WORKDIR /build
ADD ./package.json /build
RUN yarn install

ADD . /build
RUN yarn build

FROM nginx:1.13.3-alpine

RUN rm /etc/nginx/conf.d/default.conf
COPY nginx-conf/ /etc/nginx/conf.d/

WORKDIR /usr/share/nginx/html
COPY --from=0 /build/build/ .
