﻿namespace Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) was not found")
        {

        }

        public NotFoundException(string name) : base($"{name} not found")
        {

        }
    }
}
