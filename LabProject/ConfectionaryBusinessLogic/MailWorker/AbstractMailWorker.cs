﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;

namespace ConfectionaryBusinessLogic.MailWorker
{
    public abstract class AbstractMailWorker
    {
        protected string mailLogin;
        protected string mailPassword;
        protected string smtpClientHost;
        protected int smtpClientPort;
        protected string popHost;
        protected int popPort;
        private IMessageInfoLogic messageInfoLogic;

        public AbstractMailWorker(IMessageInfoLogic _messageInfoLogic)
        {
            messageInfoLogic = _messageInfoLogic;
        }

        public void MailConfig(MailConfigBindingModel config)
        {
            mailLogin = config.MailLogin;
            mailPassword = config.MailPassword;
            smtpClientHost = config.SmtpClientHost;
            smtpClientPort = config.SmtpClientPort;
            popHost = config.PopHost;
            popPort = config.PopPort;
        }

        public async void MailSendAsync(MailSendInfoBindingModel info)
        {
            if (string.IsNullOrEmpty(mailLogin) || string.IsNullOrEmpty(mailPassword))
            {
                return;
            }
            if (string.IsNullOrEmpty(smtpClientHost) || smtpClientPort == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(info.MailAddress) || string.IsNullOrEmpty(info.Subject) || string.IsNullOrEmpty(info.Text))
            {
                return;
            }
            await SendMailAsync(info);
        }

        public async void MailCheck()
        {
            if (string.IsNullOrEmpty(mailLogin) || string.IsNullOrEmpty(mailPassword))
            {
                return;
            }
            if (string.IsNullOrEmpty(popHost) || popPort == 0)
            {
                return;
            }
            if (messageInfoLogic == null)
            {
                return;
            }
            var list = await ReceiveMailAsync();
            foreach (var mail in list)
            {
                messageInfoLogic.CreateOrUpdate(mail);
            }
        }

        protected abstract Task SendMailAsync(MailSendInfoBindingModel info);
        protected abstract Task<List<MessageInfoBindingModel>> ReceiveMailAsync();
    }
}
