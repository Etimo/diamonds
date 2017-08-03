FROM node:8.2.1-alpine

RUN mkdir -p /build
WORKDIR /build
ADD . /build

RUN npm install -g -s --no-progress
RUN yarn
RUN yarn build
RUN yarn cache clean

FROM nginx:1.13.3-alpine

RUN rm /etc/nginx/conf.d/default.conf
COPY nginx-conf/ /etc/nginx/conf.d/

WORKDIR /usr/share/nginx/html
COPY --from=0 /build/build/ .
