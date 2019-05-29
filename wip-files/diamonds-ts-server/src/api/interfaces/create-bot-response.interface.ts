import { ApiModelProperty } from "@nestjs/swagger";

export class CreateBotResponse {
  @ApiModelProperty()
  id: string;
  @ApiModelProperty()
  token: string;
}
