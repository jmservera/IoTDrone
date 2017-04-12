using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace ReparaBot.Dialogs
{
    [Serializable]
    public class MyRepairDialog : IDialog<object>
    {

        // Welcome message
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Bienvenido al bot de Servicio de Mantenimiento");
            context.Wait(MessageReceivedAsync);
        }

        // Redirect to menu
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            ShowOptions(context);
        }

        // Bot options menu
        private void ShowOptions(IDialogContext context)
        {
            var choices = new[] { "Tubería rota" };
            PromptDialog.Choice(context, ChoiceSelectedAsync, choices,
                "Tengo estas incidencias abiertas:", "Porfavor, selecciona una de las incidencias abiertas");
        }

        private async Task ChoiceSelectedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            var choice = await argument;
            switch (choice)
            {
                case "Tubería rota":

                    await context.PostAsync("Se ha detectado una tubería rota");
                    PromptDialog.Confirm(context, SendPictureAsync, "¿Quieres ver una foto y resolver la incidencia?");
                    break;

                default:
                    ShowOptions(context);
                    break;
            }
        }

        private async Task SendPictureAsync(IDialogContext context, IAwaitable<bool> result)
        {
            var choice = await result;

            if (choice == true)
            {
                await context.PostAsync("De acuerdo, aquí tienes una foto y detalles:");
                CreateHeroCard(context);
                await context.PostAsync("Aqui tienes una serie de incidencias relacionadas con su resolución:");
                CreateHeroCardCarousel(context);
                CreateMapHeroCard(context);
                PromptDialog.Confirm(context, TerminatedIncidenceAsync, "¿Se ha resuelto la incidencia?");
            }
            else
            {
                await context.PostAsync("De acuerdo");
                ShowOptions(context);
            }
        }

        private async Task TerminatedIncidenceAsync(IDialogContext context, IAwaitable<bool> result)
        {
            var choice = await result;

            if (choice == true)
            {
                await context.PostAsync("De acuerdo, la incidencia se ha cerrado");
                ShowOptions(context);
            }
            else
            {
                await context.PostAsync("De acuerdo, la incidencia se mantendrá abierta");
                ShowOptions(context);
            }
        }

        public async void CreateHeroCard(IDialogContext context)
        {

            var reply = context.MakeMessage();
            reply.Attachments = new List<Attachment>();

            HeroCard hc = new HeroCard()
            {
                Title = "Tubería rota",
                Subtitle = "Microsoft Ibérica, Paseo del Club Deportivo, 1",
                Text = "Localizada una tubería rota en el auditorio de la empresa",
                Images = new List<CardImage>()
                {
                    new CardImage()
                    {
                        Url = "http://2.bp.blogspot.com/-W7k65vjl6fI/UNR-2B0xuGI/AAAAAAAAKGQ/SXpq6EChzqE/s1600/pipes.jpg"
                    }
                }
            };

            reply.Attachments.Add(hc.ToAttachment());
            await context.PostAsync(reply);
        }

        public async void CreateHeroCardCarousel(IDialogContext context)
        {

            var reply = context.MakeMessage();
            reply.AttachmentLayout = "carousel";
            reply.Attachments = new List<Attachment>();

            HeroCard hc1 = new HeroCard()
            {
                Title = "Tubería rota",
                Subtitle = "Fecha: 20/03/2017",
                Text = "Se ha cambiado la tubería por completo.",
                Images = new List<CardImage>()
                {
                    new CardImage()
                    {
                        Url = "https://i.stack.imgur.com/8yZ9x.jpg"
                    }
                }
            };

            HeroCard hc2 = new HeroCard()
            {
                Title = "Tubería rota",
                Subtitle = "Fecha: 12/02/2017",
                Text = "Se han unido los dos extremos de la tubería rota y se han unido con cinta aislante.",
                Images = new List<CardImage>()
                {
                    new CardImage()
                    {
                        Url = "https://coolestguidesontheplanet.com/wp-content/uploads/2013/12/ssh-broken-pipe.jpg"
                    }
                }
            };

            HeroCard hc3 = new HeroCard()
            {
                Title = "Tubería rota",
                Subtitle = "Fecha: 23/12/2016",
                Text = "Se ha recubierto la rotura con varios paños para evitar la pérdida de agua.",
                Images = new List<CardImage>()
                {
                    new CardImage()
                    {
                        Url = "https://www.emergencyplumber.uk.com/images/burst-pipe.png"
                    }
                }
            };

            reply.Attachments.Add(hc1.ToAttachment());
            reply.Attachments.Add(hc2.ToAttachment());
            reply.Attachments.Add(hc3.ToAttachment());
            await context.PostAsync(reply);
        }

        public async void CreateMapHeroCard(IDialogContext context)
        {

            var reply = context.MakeMessage();
            reply.Attachments = new List<Attachment>();

            List<CardAction> cardButtons = new List<CardAction>();

            CardAction plButton = new CardAction()
            {
                Value = "https://binged.it/2nDmF9V",
                Type = "openUrl",
                Title = "¿Cómo llegar?"
            };
            cardButtons.Add(plButton);

            HeroCard hc = new HeroCard()
            {
                Buttons = cardButtons,
                Images = new List<CardImage>()
                {
                    new CardImage()
                    {
                        Url = "https://s22.postimg.org/4999muc41/mapa.png"
                    }
                }
            };

            reply.Attachments.Add(hc.ToAttachment());
            await context.PostAsync(reply);

        }
    }

}