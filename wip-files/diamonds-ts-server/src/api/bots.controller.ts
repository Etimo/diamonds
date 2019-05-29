import {
  Controller,
  Post,
  Body,
  Get,
  Res,
  ConflictException,
} from "@nestjs/common";
import { ApiCreatedResponse, ApiOperation, ApiResponse } from "@nestjs/swagger";
import { CreateBotResponse } from "./interfaces/create-bot-response.interface";
import { CreateBotDto } from "./interfaces/create-bot-input.interface";

@Controller("bots")
export class BotsController {
  constructor() {}

  @Post()
  @ApiOperation({ title: "Create a new bot", description: "todo description"})
  @ApiCreatedResponse({ type: CreateBotResponse, description: "The bot was successfully created" })
  async create(@Body() input: CreateBotDto) {
  }
}
