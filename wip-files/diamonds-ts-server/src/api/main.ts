import { NestFactory } from "@nestjs/core";
import { SwaggerModule, DocumentBuilder } from "@nestjs/swagger";
import { AppModule } from "./app.module";
import bodyParser = require("body-parser");

async function bootstrap() {
  const app = await NestFactory.create(AppModule, {
    // logger: log,
  });
  app.use(bodyParser.json());

  const options = new DocumentBuilder()
    .setTitle("Diamonds")
    .setDescription("Diamonds API description")
    .setVersion("1.0")
    .build();
  const document = SwaggerModule.createDocument(app, options);
  SwaggerModule.setup("docs", app, document);

  await app.listen(3000);
}
bootstrap();
