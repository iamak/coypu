﻿using System;
using NSpec;
using NSpec.Domain;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
	internal class When_finding_an_element_by_xpath : DriverSpecs
	{
		public Action Specs(Func<Driver> driver, ActionRegister it)
		{
			return () =>
			{
				it["should find present examples"] = () =>
				{
					var shouldFind = "//*[@id = 'inspectingContent']//p[@class='css-test']/span";
					driver().FindXPath(shouldFind).Text.should_be("This");

					shouldFind = "//ul[@id='cssTest']/li[3]";
					driver().FindXPath(shouldFind).Text.should_be("Me! Pick me!");
				};

				it["should not find missing examples"] = () =>
				{
					const string shouldNotFind = "//*[@id = 'inspectingContent']//p[@class='css-missing-test']";
					Assert.Throws<MissingHtmlException>(() => driver().FindXPath(shouldNotFind), "Expected not to find something at: " + shouldNotFind);
				};

				it["should only finds visible elements"] = () =>
				{
					const string shouldNotFind = "//*[@id = 'inspectingContent']//p[@class='css-test']/img";
					Assert.Throws<MissingHtmlException>(() => driver().FindXPath(shouldNotFind), "Expected not to find something at: " + shouldNotFind);
				};
			};
		}
	}
}