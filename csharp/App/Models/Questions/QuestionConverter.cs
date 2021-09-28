using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using IP1.Samples.Models;

namespace IP1.Models.Converters
{
    public class QuestionConverter : JsonConverter<Question>
    {
        private enum TypeDiscriminator
        {
            TextBox,
            Essay,
            RadioButtons,
            Dropdown,
            MultipleDropdown,
            CheckBox,
            ValueSelection,
            ThreeEmoji,
            FiveEmoji,
            NPS,
            MultipleValueSelection,
            SatisfactionIndex,
            FileUpload,
            Geolocation,
            SingleChoiceImage,
        }

        public override bool CanConvert(Type typeToConvert) => typeof(Question).IsAssignableFrom(typeToConvert);

        public override Question Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            var propertyName = reader.GetString();
            if (propertyName != "type")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            var typeDiscriminator = (TypeDiscriminator)Enum.Parse(typeof(TypeDiscriminator), reader.GetString());
            Question question = typeDiscriminator switch
            {
                TypeDiscriminator.TextBox => new TextBoxQuestion(),
                TypeDiscriminator.Essay => new EssayQuestion(),
                TypeDiscriminator.RadioButtons => new RadioButtonsQuestion(),
                TypeDiscriminator.Dropdown => new DropdownQuestion(),
                TypeDiscriminator.MultipleDropdown => new MultipleDropdownQuestion(),
                TypeDiscriminator.CheckBox => new CheckBoxQuestion(),
                TypeDiscriminator.ValueSelection => new ValueSelectionQuestion(),
                TypeDiscriminator.ThreeEmoji => new ThreeEmojiQuestion(),
                TypeDiscriminator.FiveEmoji => new FiveEmojiQuestion(),
                TypeDiscriminator.NPS => new NPSQuestion(),
                TypeDiscriminator.MultipleValueSelection => new MultipleValueSelectionQuestion(),
                TypeDiscriminator.SatisfactionIndex => new SatisfactionIndexQuestion(),
                TypeDiscriminator.FileUpload => new FileUploadQuestion(),
                TypeDiscriminator.Geolocation => new GeolocationQuestion(),
                TypeDiscriminator.SingleChoiceImage => new SingleChoiceImageQuestion(),
                _ => throw new JsonException()
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return question;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "id":
                            question.Id = reader.GetString();
                            break;

                        case "title":
                            question.Title = reader.GetString();
                            break;

                        case "subtitle":
                            question.Subtitle = reader.GetString();
                            break;

                        case "required":
                            question.Required = reader.GetBoolean();
                            break;

                        case "commentable":
                            question.Commentable = reader.GetBoolean();
                            break;

                        case "order":
                            question.Order = reader.GetNullableInt32();
                            break;

                        case "condition":
                            question.Condition = reader.GetString();
                            break;

                        case "translations":
                            question.Translations = reader.GetTranslations(options);
                            break;
                    }

                    switch (question)
                    {
                        case TextBoxQuestion textBox:
                            switch (propertyName)
                            {
                                case "maxCharacters":
                                    textBox.MaxCharacters = reader.GetNullableInt32();
                                    break;
                            }

                            break;

                        case EssayQuestion essay:
                            switch (propertyName)
                            {
                                case "maxWords":
                                    essay.MaxWords = reader.GetNullableInt32();
                                    break;

                                case "rows":
                                    essay.Rows = reader.GetInt32();
                                    break;

                                case "columns":
                                    essay.Columns = reader.GetInt32();
                                    break;
                            }

                            break;

                        case RadioButtonsQuestion radioButtons:
                            switch (propertyName)
                            {
                                case "renderAsButtons":
                                    radioButtons.RenderAsButtons = reader.GetBoolean();
                                    break;

                                case "alternatives":
                                    radioButtons.Alternatives = reader.GetAlternatives<TextAlternative>(options);
                                    break;
                            }
                            break;

                        case MultipleDropdownQuestion multipleDropdown:
                            switch (propertyName)
                            {
                                case "alternatives":
                                    multipleDropdown.Alternatives = reader.GetAlternatives<TextAlternative>(options);
                                    break;

                                case "rows":
                                    multipleDropdown.Rows = reader.GetArray<string>(options);
                                    break;

                                case "distinct":
                                    multipleDropdown.Distinct = reader.GetBoolean();
                                    break;
                            }
                            break;

                        case DropdownQuestion dropdown:
                            switch (propertyName)
                            {
                                case "alternatives":
                                    dropdown.Alternatives = reader.GetAlternatives<TextAlternative>(options);
                                    break;
                            }
                            break;

                        case CheckBoxQuestion checkBox:
                            switch (propertyName)
                            {
                                case "min":
                                    checkBox.Min = reader.GetNullableInt32();
                                    break;

                                case "max":
                                    checkBox.Max = reader.GetNullableInt32();
                                    break;

                                case "renderAsButtons":
                                    checkBox.RenderAsButtons = reader.GetBoolean();
                                    break;

                                case "alternatives":
                                    checkBox.Alternatives = reader.GetAlternatives<TextAlternative>(options);
                                    break;
                            }
                            break;

                        case SatisfactionIndexQuestion satisfactionIndex:
                            switch (propertyName)
                            {
                                case "rows":
                                    satisfactionIndex.Rows = reader.GetArray<string>(options);
                                    break;
                            }
                            break;

                        case MultipleValueSelectionQuestion multipleValueSelection:
                            switch (propertyName)
                            {
                                case "min":
                                    multipleValueSelection.Min = reader.GetInt32();
                                    break;

                                case "max":
                                    multipleValueSelection.Max = reader.GetInt32();
                                    break;

                                case "step":
                                    multipleValueSelection.Step = reader.GetInt32();
                                    break;

                                case "variant":
                                    if (Enum.TryParse(reader.GetString(), out ValueSelectionVariant variant))
                                    {
                                        multipleValueSelection.Variant = variant;
                                    }
                                    break;

                                case "rows":
                                    multipleValueSelection.Rows = reader.GetArray<string>(options);
                                    break;
                            }
                            break;

                        case ThreeEmojiQuestion threeEmoji:
                            break;

                        case FiveEmojiQuestion fiveEmoji:
                            break;

                        case NPSQuestion nps:
                            break;

                        case ValueSelectionQuestion valueSelection:
                            switch (propertyName)
                            {
                                case "min":
                                    valueSelection.Min = reader.GetInt32();
                                    break;

                                case "max":
                                    valueSelection.Max = reader.GetInt32();
                                    break;

                                case "step":
                                    valueSelection.Step = reader.GetInt32();
                                    break;

                                case "variant":
                                    if (Enum.TryParse(reader.GetString(), out ValueSelectionVariant variant))
                                    {
                                        valueSelection.Variant = variant;
                                    }
                                    break;
                            }
                            break;

                        case FileUploadQuestion fileUpload:
                            switch (propertyName)
                            {
                                case "acceptedTypes":
                                    fileUpload.AcceptedTypes = reader.GetString();
                                    break;

                                case "maxSizeBytes":
                                    fileUpload.MaxSizeBytes = reader.GetNullableInt32();
                                    break;
                            }
                            break;

                        case GeolocationQuestion geolocation:
                            switch (propertyName)
                            {
                                case "startCoords":
                                    geolocation.StartCoords = reader.GetCoords(options);
                                    break;

                                case "startZoom":
                                    geolocation.StartZoom = reader.GetNullableInt32();
                                    break;

                                case "defaultMapType":
                                    geolocation.DefaultMapType = reader.GetEnum<MapType>(MapType.ROADMAP) ?? MapType.ROADMAP;
                                    break;

                                case "showMapTypeControl":
                                    geolocation.ShowMapTypeControl = reader.GetBoolean();
                                    break;
                            }
                            break;

                        case SingleChoiceImageQuestion imageQuestion:
                            switch (propertyName)
                            {
                                case "alternatives":
                                    imageQuestion.Alternatives = reader.GetAlternatives<ImageAlternative>(options);
                                    break;
                            }
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Question question, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("type", question.Type);

            writer.WriteString("id", question.Id);
            writer.WriteString("title", question.Title);
            writer.WriteString("subtitle", question.Subtitle);
            writer.WriteBoolean("required", question.Required);
            writer.WriteBoolean("commentable", question.Commentable);
            writer.WriteNullableNumber("order", question.Order);
            writer.WriteString("condition", question.Condition);

            switch (question)
            {
                case TextBoxQuestion textBox:
                    writer.WriteNullableNumber("maxCharacters", textBox.MaxCharacters);
                    break;

                case EssayQuestion essay:
                    writer.WriteNullableNumber("maxWords", essay.MaxWords);
                    writer.WriteNumber("rows", essay.Rows);
                    writer.WriteNumber("columns", essay.Columns);
                    break;

                case RadioButtonsQuestion radioButtons:
                    writer.WriteBoolean("renderAsButtons", radioButtons.RenderAsButtons);
                    writer.WriteAlternatives("alternatives", radioButtons.Alternatives, options);
                    break;

                case MultipleDropdownQuestion multipleDropdown:
                    writer.WriteAlternatives("alternatives", multipleDropdown.Alternatives, options);
                    writer.WriteArray("rows", multipleDropdown.Rows, options);
                    writer.WriteBoolean("distinct", multipleDropdown.Distinct);
                    break;

                case DropdownQuestion dropdown:
                    writer.WriteAlternatives("alternatives", dropdown.Alternatives, options);
                    break;

                case CheckBoxQuestion checkBox:
                    writer.WriteNullableNumber("min", checkBox.Min);
                    writer.WriteNullableNumber("max", checkBox.Max);
                    writer.WriteBoolean("renderAsButtons", checkBox.RenderAsButtons);
                    writer.WriteAlternatives("alternatives", checkBox.Alternatives, options);
                    break;

                case MultipleValueSelectionQuestion multipleValueSelection:
                    writer.WriteNumber("min", multipleValueSelection.Min);
                    writer.WriteNumber("max", multipleValueSelection.Max);
                    writer.WriteNumber("step", multipleValueSelection.Step);
                    if (multipleValueSelection.Variant.HasValue)
                    {
                        writer.WriteString("variant", multipleValueSelection.Variant.ToString());
                    }
                    writer.WriteArray("rows", multipleValueSelection.Rows, options);
                    break;

                case ValueSelectionQuestion valueSelection:
                    writer.WriteNumber("min", valueSelection.Min);
                    writer.WriteNumber("max", valueSelection.Max);
                    writer.WriteNumber("step", valueSelection.Step);
                    if (valueSelection.Variant.HasValue)
                    {
                        writer.WriteString("variant", valueSelection.Variant.ToString());
                    }
                    break;

                case FileUploadQuestion fileUpload:
                    writer.WriteString("acceptedTypes", fileUpload.AcceptedTypes);
                    writer.WriteNullableNumber("maxSizeBytes", fileUpload.MaxSizeBytes);
                    break;

                case GeolocationQuestion geolocation:
                    writer.WriteCoords("startCoords", geolocation.StartCoords, options);
                    writer.WriteNullableNumber("startZoom", geolocation.StartZoom);
                    writer.WriteString("defaultMapType", geolocation.DefaultMapType.ToString().ToLower());
                    writer.WriteBoolean("showMapTypeControl", geolocation.ShowMapTypeControl);
                    break;

                case SingleChoiceImageQuestion imageQuestion:
                    writer.WriteAlternatives("alternatives", imageQuestion.Alternatives, options);
                    break;
            }

            writer.WriteTranslations("translations", question.Translations, options);

            writer.WriteEndObject();
        }
    }
}
